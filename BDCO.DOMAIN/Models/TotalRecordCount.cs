using BDCO.Domain.RequestModels;
using System.Linq;

namespace BDCO.Domain.Aggregates
{
    public class TotalRecordCount : AggregateRoot
    {
        public long TotalRecord { get; set; }

        public UnitOfWork _unitOfWork = new UnitOfWork();

        public long GetTotalRecordCount(TotalRecordCountRM objTotal)
        {


            //string sql = string.Format("SELECT CAST( COUNT(*) AS BIGINT) AS TotalRecord FROM {0} WHERE UserId='{1}'", .TableName, .UserId);
            string sql = "";
            if (objTotal.GeoPacket == null)
            {
                if (objTotal.TableName == "CatchmentArea")
                {
                    sql = string.Format(@"DECLARE @UserId int                                                                       
                                            SET @UserId =2
                                            SELECT CAST( COUNT(*) AS BIGINT) AS TotalRecord  
                                            FROM CatchmentArea where ServicePointId in ( SELECT ServicePointId 
                                            FROM UserServicePoint WHERE(UserId=@UserId OR @UserId=0))", objTotal.TableName, objTotal.UserId);
                }
                else if (objTotal.TableName == "MemberInfo")
                {
                    sql = string.Format(@"SELECT CAST( COUNT(*) AS BIGINT) AS TotalRecord FROM UserGeoLocation  UG
                    INNER JOIN MemberInfo MI ON (UG.DistrictCode = MI.DistrictCode OR UG.DistrictCode = 0) AND (UG.UpazilaCode = MI.UpazilaCode OR UG.UpazilaCode = 0) AND (UG.UnionCode = MI.UnionCode OR UG.UnionCode = 0) AND (UG.VillageCode = MI.VillageCode OR UG.VillageCode = 0)  AND (UG.BlockId = MI.BlockId OR UG.BlockId = 0)
                    WHERE UG.UserID = {0}", objTotal.UserId);
                }
                else
                {
                    sql = string.Format(@"DECLARE @UserId int
                                            DECLARE @ServicePointId nvarchar(12)
                                            DECLARE @DistrictCode nvarchar(5)
                                            DECLARE @UpazilaCode nvarchar(5)
                                            DECLARE @UnionCode nvarchar(5)
                                            DECLARE @VillageCode nvarchar(5)                                           

                                            SET @UserId ={1}
	                                        SET @ServicePointId =''
	                                        SET @DistrictCode =''
	                                        SET @UpazilaCode =''
	                                        SET @UnionCode =''
	                                        SET @VillageCode ='' 

                                            SELECT CAST( COUNT(*) AS BIGINT) AS TotalRecord FROM {0} H 
                                            INNER JOIN UserGeoLocation  UG ON (UG.DistrictCode = H.DistrictCode OR UG.DistrictCode = 0) AND (UG.UpazilaCode = H.UpazilaCode OR UG.UpazilaCode = 0) AND (UG.UnionCode = H.UnionCode OR UG.UnionCode = 0) AND (UG.VillageCode = H.VillageCode OR UG.VillageCode = 0)  
                                            WHERE 
                                            (UG.UserId=@UserId OR @UserId=0) AND
                                            --(H.ServicePointId=@ServicePointId or @ServicePointId='') AND 
                                            (H.DistrictCode=@DistrictCode or @DistrictCode='') AND
                                            (H.UpazilaCode=@UpazilaCode or @UpazilaCode='') AND
                                            (H.UnionCode=@UnionCode or @UnionCode='') AND
                                            (H.VillageCode=@VillageCode or @VillageCode='') ", objTotal.TableName, objTotal.UserId);
                }

            }
            else
            {
                sql = string.Format($@"DECLARE @UserId int
                                            DECLARE @ServicePointId nvarchar(12)
                                            DECLARE @DistrictCode nvarchar(5)
                                            DECLARE @UpazilaCode nvarchar(5)
                                            DECLARE @UnionCode nvarchar(5)
                                            DECLARE @VillageCode nvarchar(5)                                           

                                            SET @UserId ='{objTotal.UserId}'	                                       
	                                        SET @DistrictCode ='{objTotal.GeoPacket.DistrictCode}'
	                                        SET @UpazilaCode ='{objTotal.GeoPacket.UpazilaCode}'
	                                        SET @UnionCode ='{objTotal.GeoPacket.UnionCode}'
	                                        SET @VillageCode ='{objTotal.GeoPacket.VillageCode}' 

                                            SELECT CAST( COUNT(*) AS BIGINT) AS TotalRecord FROM {objTotal.TableName} H
                                            INNER JOIN UserGeoLocation  UG ON (UG.DistrictCode = H.DistrictCode OR UG.DistrictCode = 0) AND (UG.UpazilaCode = H.UpazilaCode OR UG.UpazilaCode = 0) AND (UG.UnionCode = H.UnionCode OR UG.UnionCode = 0) AND (UG.VillageCode = H.VillageCode OR UG.VillageCode = 0)  
                                            WHERE 
                                            (UG.UserId=@UserId OR @UserId=0) AND                                             
                                            (H.DistrictCode=@DistrictCode or @DistrictCode='') AND
                                            (H.UpazilaCode=@UpazilaCode or @UpazilaCode='') AND
                                            (H.UnionCode=@UnionCode or @UnionCode='') AND
                                            (H.VillageCode=@VillageCode or @VillageCode='') ");
            }
            var result = _unitOfWork.context.Database.SqlQuery<TotalRecordCount>(sql).FirstOrDefault();
            return result.TotalRecord;
        }
    }
}
