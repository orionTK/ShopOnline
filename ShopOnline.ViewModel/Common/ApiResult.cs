using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.Common
{
    public class ApiResult<T>
    {
        //public ApiResult(bool isSuccessed, string message) 
        //{
        //    IsSuccessed = isSuccessed;
        //    Message = message;
        //}

        

        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
    }
}
