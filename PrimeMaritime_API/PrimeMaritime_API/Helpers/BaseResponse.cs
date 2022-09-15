﻿using PrimeMaritime_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Helpers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public T Data { get; set; }
    }

    public class PagedResponse<T> : Response<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public PagedResponse(T data, int totalCount, int pageSize)
        {
            this.TotalCount = totalCount;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
