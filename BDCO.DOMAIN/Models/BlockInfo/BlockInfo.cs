using BDCO.Core.Command;
using BDCO.Domain.Aggregates;
using BDCO.Domain.RequestParams;
using BDCO.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BDCO.Domain.Models
{
    public class BlockInfo:AggregateRoot
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlockId { get; set; }       
        public string BlockName { get; set; }
        public string BlockFullName { get; set; }       
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public int CampId { get; set; }
        public int CenterId { get; set; }

        private UnitOfWork _unitOfWork = new UnitOfWork();
        public CommandResult Save(BlockInfo command)
        {
            try
            {
                int blockId = command.BlockId;               
                string result = "";
                BlockInfo block = new BlockInfo();
                Tools.CopyClass(block, command);
                var IsExist = _unitOfWork.GenericRepositories<BlockInfo>().FindBy(x => x.BlockId == command.BlockId)?.FirstOrDefault();
                if (IsExist == null)
                {
                    _unitOfWork.GenericRepositories<BlockInfo>().Insert(block);
                    result = _unitOfWork.SaveChange();
                    LogEventStore(block.BlockId.ToString(), GetType().Name, "Save");
                }
                else
                {
                    block.BlockId = IsExist.BlockId;
                    _unitOfWork.GenericRepositories<BlockInfo>().Update(block);
                    result = _unitOfWork.SaveChange();
                    LogEventStore(block.BlockId.ToString(), GetType().Name, "Update");
                }
                return ResponseBase.CommandResultSuccess(result, blockId, block.BlockId);
            }
            catch (Exception ex)
            {
                return ResponseBase.CommandResultError(ex.Message, command.BlockId);
            }
        }
        public List<BlockInfo> GetAllBlock(BlockRequestParams query)
        {
            List<BlockInfo> aBlockList = new List<BlockInfo>();
            string sqlQuery = string.Format("SELECT * FROM BlockInfo WHERE DistrictCode='{0}' AND UpazilaCode='{1}' AND UnionCode='{2}' AND VillageCode='{3}'", query.DistrictCode, query.UpazilaCode, query.UnionCode, query.VillageCode);
            aBlockList = _unitOfWork.GenericRepositories<BlockInfo>().GetRecordSet(sqlQuery).ToList();
            return aBlockList;
        }
        public List<BlockInfo> GetAllBlock()
        {
            List<BlockInfo> aBlockList = new List<BlockInfo>();
            string sqlQuery = string.Format("SELECT * FROM BlockInfo");
            aBlockList = _unitOfWork.GenericRepositories<BlockInfo>().GetRecordSet(sqlQuery).ToList();
            return aBlockList;
        }
        public List<BlockStoreProcedure> Searchblock(BlockStoreProcedure block)
        {
            string sql = $"EXEC SearchBlockInfo '{block.BlockName}','{block.CampId}','{block.CenterId}','{block.DistrictCode}','{block.UpazilaCode}','{block.UnionCode}','{block.VillageCode}' , '{block.PageNo}'";
            var result = _unitOfWork.RawSqlQuery<BlockStoreProcedure>(sql).ToList();
            return result;
        }
        public BlockInfo GetSingle(int recordId)
        {
            return _unitOfWork.GenericRepositories<BlockInfo>().FindBy(x => x.BlockId == recordId).FirstOrDefault();
        }

        public List<UserBlockModel> APIGetBlockList(ForApiResponse query)
        {
            string blockSql = string.Format(@"SELECT sb.BlockId ,sb.BlockName as BlockName, sb.BlockName+' ('+c.CampName+')' as 'BlockFullName', UG.DistrictCode ,UG.UpazilaCode ,UG.UnionCode ,UG.VillageCode , SB.CenterId, UG.CampId 
                                                    FROM UserGeolocation UG 
                                                    INNER JOIN BlockInfo sb on (UG.DistrictCode = sb.DistrictCode OR UG.DistrictCode = 0) AND (UG.UpazilaCode = sb.UpazilaCode OR sb.UpazilaCode = 0) AND (UG.UnionCode = sb.UnionCode OR UG.UnionCode = 0) AND (UG.VillageCode = sb.VillageCode OR UG.VillageCode = 0) 
                                                    LEFT JOIN CampInfo C ON sb.CampId = C.CampId
                                                    WHERE UG.UserId = {0}",query.UserId);
            return _unitOfWork.GenericRepositories<UserBlockModel>().GetRecordSet(blockSql).ToList();
        }
    }
    public class BlockRequestParams
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
    }
    public class BlockStoreProcedure
    {
        public int? BlockId { get; set; }        
        public string BlockName { get; set; }
        public int CampId { get; set; }
        public int CenterId { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string CampName { get; set; }
        public string CenterName { get; set; }
        public int? TotalRecord { get; set; }
        public int? PageNo { get; set; }
    }
}
