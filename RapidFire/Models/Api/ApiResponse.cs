﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidFireLib.Models.Api
{
    /// <summary>
    /// use for fetch data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            ApiPacket = new ApiPacket<T>();
        }
        public string Command { get; set; }
        public bool Success { get; set; }
        public String Message { get; set; }
        public long ServerRecordId { get; set; }
        public long RecordId { get; set; }
        public long TotalRecord { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public ApiPacket<T> ApiPacket { get; set; }
        public int Status { get; set; }
    }
    /// <summary>
    /// Use for sync Data
    /// </summary>
    public class ApiResponse
    {
        public bool Success { get; set; }
        public String Message { get; set; }
        public long ServerRecordId { get; set; }
        public object RecordId { get; set; }
        public int Status { get; set; }
        public string CallerId { get; set; }
        public string Command { get; set; }

    }
}