﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerMonitor.Models
{
    /// <summary>
    ///  自定义请求处理器 类似filter 创建者:xb 创建时间: 2018/01
    /// </summary>
    class CustomHandler : DelegatingHandler
    {
        /// <summary>
        /// 拦截请求 创建者:xb 创建时间: 2018/01
        /// </summary>
        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 在此处理 HttpRequestMessage 对象。
            Debug.WriteLine("Processing request throught Custom Handler 1");
            Debug.WriteLine(string.Format("RequestUri:{0}\t", request.RequestUri));

            // 一旦前步骤完成，调用 DelegatingHandler.SendAsync 继续将其传递至 inner handler
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            // 在此处理返回的 HttpResponseMessage 对象。
            Debug.WriteLine("Processing response throught Custom Handler 1");

            return response;
        }
    }
}
