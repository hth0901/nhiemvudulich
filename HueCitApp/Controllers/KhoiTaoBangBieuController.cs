using Application.DiaDiemDuLich;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.SuKien;
using Microsoft.AspNetCore.Hosting;
using Application.Core;
using Application.MonAnThucUong;
using Domain.ResponseEntity;
using Domain.HueCit;
using System.Drawing.Printing;
using Domain;
using System.Collections.Generic;

namespace HueCitApp.Controllers
{
    public class KhoiTaoBangBieuController : BaseApiController
    {
        public KhoiTaoBangBieuController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
        [HttpGet("bangdulieutainguyen")]
        [AllowAnonymous]
        public async Task<IActionResult> BangDuLieuTaiNguyen(CancellationToken ct)
        {
            List<object> GetPropertiesNameOfClass(object pObject)
            {
                List<object> propertyList = new List<object>();
                if (pObject != null)
                {
                    foreach (var prop in pObject.GetType().GetProperties())
                    {
                        
                        var single = new
                        {
                            name = prop.Name,
                            dataType = prop.PropertyType.Name

                        };
                        propertyList.Add(single);


                    }
                }               
                return propertyList;
            }

            var _object = new DL_BangTaiNguyen();
            List<object> listResult = GetPropertiesNameOfClass(_object);
            var finalList = new
            {
                type = "detail",
                name = "TaiNguyenDuLich",
                version = 0,
                schema = listResult
            };
            return HandlerResult(Result<object>.Success(finalList));
        }

        [HttpGet("bangdulieudulich")]
        [AllowAnonymous]
        public async Task<IActionResult> BangDuLieuDuLich(CancellationToken ct)
        {
            List<object> GetPropertiesNameOfClass(object pObject)
            {
                List<object> propertyList = new List<object>();
                if (pObject != null)
                {
                    foreach (var prop in pObject.GetType().GetProperties())
                    {

                        var single = new
                        {
                            name = prop.Name,
                            dataType = prop.PropertyType.Name

                        };
                        propertyList.Add(single);


                    }
                }
                return propertyList;
            }

            var _object = new DL_BangDuLich();
            List<object> listResult = GetPropertiesNameOfClass(_object);
            var finalList = new
            {
                type = "detail",
                name = "DuLieuDuLich",
                version = 0,
                schema = listResult
            };
            return HandlerResult(Result<object>.Success(finalList));
        }

        [HttpGet("bangdulieudichvu")]
        [AllowAnonymous]
        public async Task<IActionResult> BangDuLieuDichVu(CancellationToken ct)
        {
            List<object> GetPropertiesNameOfClass(object pObject)
            {
                List<object> propertyList = new List<object>();
                if (pObject != null)
                {
                    foreach (var prop in pObject.GetType().GetProperties())
                    {

                        var single = new
                        {
                            name = prop.Name,
                            dataType = prop.PropertyType.Name

                        };
                        propertyList.Add(single);


                    }
                }
                return propertyList;
            }

            var _object = new DL_BangDichVu();
            List<object> listResult = GetPropertiesNameOfClass(_object);
            var finalList = new
            {
                type = "detail",
                name = "DuLieuDichVu",
                version = 0,
                schema = listResult
            };
            return HandlerResult(Result<object>.Success(finalList));
        }
        [HttpGet("bangtailieulienquan")]
        [AllowAnonymous]
        public async Task<IActionResult> BangTaiLieuLienQuan(CancellationToken ct)
        {
            List<object> GetPropertiesNameOfClass(object pObject)
            {
                List<object> propertyList = new List<object>();
                if (pObject != null)
                {
                    foreach (var prop in pObject.GetType().GetProperties())
                    {

                        var single = new
                        {
                            name = prop.Name,
                            dataType = prop.PropertyType.Name

                        };
                        propertyList.Add(single);


                    }
                }
                return propertyList;
            }

            var _object = new DL_BangTaiLieuLienQuan();
            List<object> listResult = GetPropertiesNameOfClass(_object);
            var finalList = new
            {
                type = "detail",
                name = "TaiLieuLienQuan",
                version = 0,
                schema = listResult
            };
            return HandlerResult(Result<object>.Success(finalList));
        }
    }
}
