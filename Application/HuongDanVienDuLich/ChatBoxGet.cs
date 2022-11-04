using Application.Core;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HuongDanVienDuLich
{
    public class ChatBoxGet
    {
        
    
        public class Query : IRequest<Result<String>>
        {// su li tham so dau vao
          

        }

        public class Handler : IRequestHandler<Query, Result<String>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<String>> Handle(Query request, CancellationToken cancellationToken)
            {
               
                    var result = "<script>!(function () {\r\n        let e = document.createElement(\"script\"),\r\n          t = document.head || document.getElementsByTagName(\"head\")[0];\r\n        (e.src =\r\n          \"https://cdn.jsdelivr.net/npm/rasa-webchat@1.x.x/lib/index.js\"),\r\n          // Replace 1.x.x with the version that you want\r\n          (e.async = !0),\r\n          (e.onload = () => {\r\n            window.WebChat.default(\r\n              {\r\n                customData: { language: \"en\" },\r\n                socketUrl: \"https://chatbot.huecit.com/\",\r\n                // add other props here\r\n              },\r\n              null\r\n            );\r\n          }),\r\n          t.insertBefore(e, t.firstChild);\r\n      })();\r\n    </script>";
                    return Result<String>.Success(result);

                

            }
        }
    }
}

