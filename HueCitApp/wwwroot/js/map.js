const MAP_SR = 'PROJCS["dh10000",GEOGCS["dh10000",DATUM["D_WGS_1984",SPHEROID["WGS_1984",6378137.0,298.257223563]],PRIMEM["Greenwich",0.0],UNIT["Degree",0.0174532925199433]],PROJECTION["Transverse_Mercator"],PARAMETER["False_Easting",500000.0],PARAMETER["False_Northing",0.0],PARAMETER["Central_Meridian",108.0],PARAMETER["Scale_Factor",0.9999],PARAMETER["Latitude_Of_Origin",0.0],UNIT["Meter",1.0]],VERTCS["VN2000",DATUM["D_WGS_1984",SPHEROID["WGS_1984",6378137.0,298.257223563]],PARAMETER["Vertical_Shift",0.0],PARAMETER["Direction",1.0],UNIT["Meter",1.0]]';

let polylineGraphic = null;
let graphicsLayer = null;
let currentLayerSearch = null;

const linhVucData = [
    {
        id: "00",
        linhVucId: 1
    },
    {
        id: "01",
        linhVucId: 2
    },
    {
        id: "02",
        linhVucId: 3
    },
    {
        id: "03",
        linhVucId: 4
    },
    {
        id: "04",
        linhVucId: 5
    },
    {
        id: "05",
        linhVucId: 9
    },
    {
        id: "06",
        linhVucId: 10
    },
    {
        id: "07",
        linhVucId: 11
    },
    {
        id: "08",
        linhVucId: 12
    },
    {
        id: "09",
        linhVucId: 15
    },
];

const dataDirection = [
    {
        en: "bear right onto ramp",
        vi: "sang phải vào trên đoạn đường nối",
    },
    {
        en: "turn right onto ramp",
        vi: "rẽ phải vào đoạn đường nối",
    },
    {
        en: "make sharp right onto ramp",
        vi: "sang trái ngay trên đoạn đường nối",
    },
    {
        en: "make sharp left onto ramp",
        vi: "ra trái vào đoạn đường nối",
    },
    {
        en: "turn left onto ramp",
        vi: "rẽ trái vào đoạn đường nối",
    },
    {
        en: "bear left onto ramp",
        vi: "sang trái vào đoạn đường nối",
    },
    {
        en: "take ramp on the left",
        vi: "đi dốc ở bên trái",
    },
    {
        en: "take ramp on the right",
        vi: "đi dốc ở bên phải",
    },
    {
        en: "stay on ferry for",
        vi: "ở lại phà cho",
    },
    {
        en: "on the right",
        vi: "phía bên phải",
    },
    {
        en: "on the left",
        vi: "phía bên trái",
    },
    {
        en: "northeast",
        vi: "hướng Đông Bắc",
    },
    {
        en: "southeast",
        vi: "hướng Đông Nam",
    },
    {
        en: "southwest",
        vi: "hướng Tây Nam",
    },
    {
        en: "northwest",
        vi: "hướng Tây Bắc",
    },
    {
        en: "east",
        vi: "hướng đông",
    },
    {
        en: "north",
        vi: "hướng Bắc",
    },
    {
        en: "south",
        vi: "hướng Nam",
    },
    {
        en: "west",
        vi: "hướng Tây",
    },
    {
        en: "at exit",
        vi: "ở lối ra",
    },
    {
        en: "go back",
        vi: "quay lại",
    },
    {
        en: "take ramp",
        vi: "theo đường dốc",
    },
    {
        en: "straight",
        vi: "đi thẳng",
    },
    {
        en: "and go on",
        vi: "và tiến lên",
    },
    {
        en: "bear right",
        vi: "sang bên phải",
    },
    {
        en: "bear left",
        vi: "sang bên trái",
    },
    {
        en: "turn right",
        vi: "rẽ phải",
    },
    {
        en: "turn left",
        vi: "rẽ trái",
    },
    {
        en: "near left",
        vi: "gần phía bên trái",
    },
    {
        en: "near right",
        vi: "gần phía bên phải",
    },
    {
        en: "after",
        vi: "sau",
    },
    {
        en: "at",
        vi: "tại",
    },
    {
        en: "before",
        vi: "trước",
    },
    {
        en: "onto",
        vi: "trên",
    },
    {
        en: "over",
        vi: "kết thúc",
    },
    {
        en: "past",
        vi: "vừa qua",
    },
    {
        en: "through",
        vi: "xuyên qua",
    },
    {
        en: "under",
        vi: "Dưới",
    },
    {
        en: "by",
        vi: "qua",
    },
    {
        en: "near",
        vi: "ở gần",
    },
    {
        en: "start",
        vi: "bắt đầu",
    },
    {
        en: "finish",
        vi: "kết thúc",
    },
    {
        en: "arrive",
        vi: "đến",
    },
    {
        en: "depart",
        vi: "khởi hành",
    },
    {
        en: "take",
        vi: "giữ",
    },
    {
        en: "go",
        vi: "đi",
    },
    {
        en: "distance",
        vi: "khoảng cách",
    },
    {
        en: "keep",
        vi: "giữ",
    },
    {
        en: "turn",
        vi: "rẽ",
    },
    {
        en: "right",
        vi: "bên phải",
    },
    {
        en: "left",
        vi: "bên trái",
    },
    {
        en: "first",
        vi: "đầu tiên",
    },
    {
        en: "second",
        vi: "thứ hai",
    },
    {
        en: "third",
        vi: "thứ ba",
    },
    {
        en: "fourth",
        vi: "thứ tư",
    },
    {
        en: "fifth",
        vi: "thứ năm",
    },
    {
        en: "sixth",
        vi: "thứ sáu",
    },
    {
        en: "seventh",
        vi: "thứ bảy",
    },
    {
        en: "eighth",
        vi: "thứ tám",
    },
    {
        en: "ninth",
        vi: "thứ chín",
    },
    {
        en: "tenth",
        vi: "thứ mười",
    },
    {
        en: "continue",
        vi: "tiếp tục",
    },
    {
        en: "head",
        vi: "đi thẳng",
    },
    {
        en: "on",
        vi: "trên",
    },
    {
        en: "in",
        vi: "trong",
    },
]

$(document).ready(function () {
    $(".select2").select2({
        width: '100%',
    });

    $(".select2-hidesearch").select2({
        width: '100%',
        minimumResultsForSearch: -1
    });

    $.ajax({
        type: 'get',
        async: false,
        url: '/api/DiaPhuong/diaphuong/1',
        success: function (data) {
            $(`#filter-ddl-ch-slt`).empty();
            $('#filter-ddl-ch-slt').append(`<option value=${-1}>Quận/Huyện/Thị xã</option>`)
            data.forEach(element => {
                $('#filter-ddl-ch-slt').append(`<option value=${element.id}>${element.tenDiaPhuong}</option>`)
            });
        },
        error: function (err) {
            alert('Lỗi.');
        }
    });

    $('#filter-ddl-ch-slt').on('change', function () {
        const id = $('#filter-ddl-ch-slt').val();
        if (id && id != -1) {
            $.ajax({
                type: 'get',
                async: false,
                url: '/api/DiaPhuong/diaphuong/' + id,
                success: function (data) {
                    $(`#filter-ddl-cx-slt`).empty();
                    $('#filter-ddl-cx-slt').append(`<option value=${-1}>Xã/phường/thị trấn</option>`)
                    data.forEach(element => {
                        $('#filter-ddl-cx-slt').append(`<option value=${element.id}>${element.tenDiaPhuong}</option>`)
                    });
                },
                error: function (err) {
                    alert('Lỗi.');
                }
            });
        } else if (id == -1) {
            $(`#filter-ddl-cx-slt`).empty();
            $('#filter-ddl-cx-slt').append(`<option value=${-1}>Xã/phường/thị trấn</option>`)
        }
    });

    $("#filter-ddl-ch-slt").val($("#filter-ddl-ch-slt option:first").val()).trigger('change');

    //$("#bk-input").keyup(function () {
    //    if (!$(this).val() || (parseInt($(this).val()) < 5)) {
    //        $(this).val(5)
    //    } else if (!$(this).val() || (parseInt($(this).val()) > 5000)) {
    //        $(this).val(5000)
    //    }
    //});

    require([
        "esri/config",
        "esri/intl",
        "esri/Map",
        "esri/views/MapView",
        "esri/layers/FeatureLayer",
        "esri/layers/MapImageLayer",
        "esri/layers/TileLayer",
        "esri/Basemap",
        "esri/widgets/BasemapGallery",
        "esri/widgets/Expand",
        "esri/widgets/Measurement",
        "esri/widgets/Search",
        "esri/widgets/Locate",
        "esri/widgets/Popup",
        "esri/Graphic",
        "esri/layers/GraphicsLayer",
        "esri/geometry/Point",
        "esri/geometry/Polyline",
        "esri/geometry/SpatialReference",
        "esri/geometry/projection",
        "esri/rest/support/Query"
    ], function (esriConfig, intl, Map, MapView, FeatureLayer, MapImageLayer, TileLayer, Basemap, BasemapGallery, Expand, Measurement, Search, Locate, Popup, Graphic, GraphicsLayer, Point, Polyline, SpatialReference, projection, Query) {
        const mApiKey = 'AAPKf17a300c1b284fca90afee0ecdd6fbdfqxum1STGOMVETG7zr4IxM10ieTxpbtbZGXvDaq01a9c54BH-vHhQY7Zg3WknPs40';
        esriConfig.apiKey = mApiKey;

        intl.setLocale("vi");

        //Layer Group

        const baseMap = new Basemap({
            baseLayers: [
                new TileLayer({
                    url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BDHC/BASEMAPHUE/MapServer",
                    title: "Bản đồ hành chính"
                })
            ],
            title: "Bản đồ hành chính",
            id: "bhcmap"
        });

        const baseMapQuanHuyen = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDoanhNghiep/DoanhNghiep_ThuaThienHue/FeatureServer/4",
            id: "100",
            visible: false
        });

        const baseMapPhuongXa = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDoanhNghiep/DoanhNghiep_ThuaThienHue/FeatureServer/5",
            id: "101",
            visible: false
        });

        //LuuTru
        const layer_0 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/0",
            id: "00",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sonha",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sodienthoa",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //CongTyLuHanh
        const layer_1 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/1",
            id: "01",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sonha",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sodienthoa",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //CoSoMuaSam
        const layer_2 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/2",
            id: "02",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sonha",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sodienthoa",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //CoSoAnUong - NhaHang
        const layer_3 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/3",
            id: "03",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sonha",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sodienthoa",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //DiemDuLich
        const layer_4 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/4",
            id: "04",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sonha",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sodienthoa",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //VuiChoiGiaiTri
        const layer_5 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/5",
            id: "05",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sonha",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sodienthoa",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //ChamSocSucKhoe
        const layer_6 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/6",
            id: "06",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sonha",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sodienthoa",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //TheThao
        const layer_7 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/7",
            id: "07",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sonha",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sodienthoa",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //VanChuyen
        const layer_8 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/8",
            id: "08",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sonha",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sodienthoa",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //DiSan
        const layer_9 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/9",
            id: "09",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sonha",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "sodienthoa",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //DiemGiaoDich
        const layer_10 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/10",
            id: "10",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{tendiadiem}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "tendiadiem",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "diachi",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "dienthoai",
                                "label": "Số điện thoại",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //DiemVeSinh
        const layer_11 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/11",
            id: "11",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{ten}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "ten",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "vitri",
                                "label": "Vị trí",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "hientrang",
                                "label": "Hiện trạng",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //LeHoi
        const layer_12 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/12",
            id: "12",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{tenlehoi}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "tenlehoi",
                                "label": "Tên",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "diadiem",
                                "label": "Địa điểm",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "noidung",
                                "label": "Nội dung",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //PhanAnhHienTruong
        const layer_13 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/13",
            id: "13",
            outFields: ["*"],
            popupEnabled: true,
            popupTemplate: {
                title: '{tendoituon}',
                content: [
                    {
                        "type": "fields",
                        "fieldInfos": [
                            {
                                "fieldName": "tieude",
                                "label": "Tiêu đề",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            },
                            {
                                "fieldName": "diachisuki",
                                "label": "Địa chỉ",
                                "visible": true,
                                "stringFieldOption": "text-box"
                            }
                        ]
                    }
                ],
                actions: [
                    {
                        title: "Chỉ đường",
                        id: "routes",
                        className: "esri-icon-directions",
                        type: "button",
                    },
                ],
            },
            spatialReference: new SpatialReference({
                wkt: MAP_SR
            })
        });

        //Layer Group

        const map = new Map({
            basemap: 'arcgis-topographic',
            layers: [baseMapQuanHuyen, baseMapPhuongXa, layer_0, layer_1, layer_2, layer_3, layer_4, layer_5, layer_6, layer_7, layer_8, layer_9, layer_10, layer_11, layer_12, layer_13],
        });

        graphicsLayer = new GraphicsLayer();
        map.add(graphicsLayer);

        const view = new MapView({
            map: map,
            center: [107.58513469042099, 16.462504990071924],
            zoom: 10,
            container: "viewDiv",
            popup: {
                dockEnabled: true,
                dockOptions: {
                    buttonEnabled: false,
                    breakpoint: false,
                    position: "bottom-right"
                }
            },
        });

        const routerPopup = new Popup({
            view: view,
            content: "",
            actions: [],
            title: 'Hướng dẫn chỉ đường',
            container: routerdesc
        });

        routerPopup.viewModel.includeDefaultActions = false;

        view.ui.add(routerPopup, {
            position: "top-left"
        });

        const myInterval = setInterval(function () {
            if (map.allLayers.items.some(el => el.type === 'feature')) {
                clearInterval(myInterval);
                const groupData = [
                    {
                        id: 1,
                        fid: "1",
                        text: "Điểm du lịch",
                        child: [
                            {
                                id: 1,
                                fid: "1_1",
                                lid: "04",
                                tid: "561",
                                text: "Văn hóa",
                            },
                            {
                                id: 2,
                                fid: "1_2",
                                lid: "04",
                                tid: "562",
                                text: "Làng nghề truyền thống",
                            },
                            {
                                id: 3,
                                fid: "1_3",
                                lid: "04",
                                tid: "563",
                                text: "Thiên nhiên - Khám phá",
                            },
                            {
                                id: 4,
                                fid: "1_4",
                                lid: "04",
                                tid: "564",
                                text: "Tâm linh",
                            },
                            {
                                id: 5,
                                fid: "1_5",
                                lid: "04",
                                tid: "565",
                                text: "Lịch sử",
                            },
                        ]
                    },
                    {
                        id: 2,
                        fid: "2",
                        text: "Du lịch",
                        child: [
                            {
                                id: 1,
                                fid: "2_1",
                                lid: "00",
                                text: "Lưu trú",
                            },
                            {
                                id: 2,
                                fid: "2_2",
                                lid: "01",
                                text: "Lữ hành",
                            },
                        ]
                    },
                    {
                        id: 3,
                        fid: "3",
                        text: "Dịch vụ",
                        child: [
                            {
                                id: 1,
                                fid: "3_1",
                                lid: "03",
                                text: "Nhà hàng",
                            },
                            {
                                id: 2,
                                fid: "3_2",
                                lid: "02",
                                text: "Mua sắm",
                            },
                            {
                                id: 3,
                                fid: "3_3",
                                lid: "05",
                                text: "Vui chơi giải trí",
                            },
                            {
                                id: 4,
                                fid: "3_4",
                                lid: "06",
                                text: "Chăm sóc sức khỏe",
                            },
                            {
                                id: 5,
                                fid: "3_5",
                                lid: "07",
                                text: "Thể thao",
                            },
                            {
                                id: 6,
                                fid: "3_6",
                                lid: "08",
                                text: "Vận chuyển",
                            },
                            {
                                id: 7,
                                fid: "3_7",
                                lid: "11",
                                text: "Vệ sinh công cộng",
                            },
                        ]
                    },
                    {
                        id: 4,
                        fid: "4",
                        lid: "12",
                        text: "Lễ hội",
                        child: []
                    },
                    {
                        id: 5,
                        fid: "5",
                        lid: "13",
                        text: "Phản ánh hiện trường",
                        child: []
                    },
                ];

                groupData.forEach((el) => {
                    $('#filter-lv-slt').append(
                        `<option value="${el.id}">${el.text}</option>`
                    );
                })

                $('#filter-lv-slt').on('change', function () {
                    const slt = $(this).val();
                    $('#filter-lh-slt').empty();
                    const found = groupData.find(x => x.id == slt);
                    if (found) {
                        if (found.child && found.child.length > 0) {
                            if (($('#filter-lh-slt-wrapper').hasClass('d-none'))) {
                                $('#filter-lh-slt-wrapper').removeClass('d-none');
                            }

                            found.child.forEach((el) => {
                                $('#filter-lh-slt').append(
                                    `<option value="${el.fid}">${el.text}</option>`
                                );
                            });

                            $('#filter-lh-slt').val($("#filter-lh-slt option:first").val()).trigger('change');
                        }
                        else {
                            $('#filter-lh-slt-wrapper').addClass('d-none');
                        }
                        //An khi chon PhanAnh
                        if (found.lid == "13") {
                            if (!($('#filter-ch-wrapper').hasClass('d-none'))) {
                                $('#filter-ch-wrapper').addClass('d-none')
                            }

                            if (!($('#filter-cx-wrapper').hasClass('d-none'))) {
                                $('#filter-cx-wrapper').addClass('d-none')
                            }

                            if (!($('#filter-td-wrapper').hasClass('col-12'))) {
                                $('#filter-td-wrapper').removeClass('col-lg-4')
                                $('#filter-td-wrapper').addClass('col-12')
                            }

                            $('#filter-ddl-ch-slt').val(-1).trigger('change');
                            $('#filter-ddl-cx-slt').val(-1).trigger('change');
                        } else {
                            if (($('#filter-ch-wrapper').hasClass('d-none'))) {
                                $('#filter-ch-wrapper').removeClass('d-none')
                            }

                            if (($('#filter-cx-wrapper').hasClass('d-none'))) {
                                $('#filter-cx-wrapper').removeClass('d-none')
                            }

                            if (($('#filter-td-wrapper').hasClass('col-12'))) {
                                $('#filter-td-wrapper').removeClass('col-12')
                                $('#filter-td-wrapper').addClass('col-lg-4')
                            }
                        }
                    }
                })

                $("#filter-lv-slt").val($("#filter-lv-slt option:first").val()).trigger('change');

                $('#filter-lh-slt').on('change', function () {
                    const lv = $("#filter-lv-slt").val();
                    const slt = $(this).val();
                    
                    const found = groupData.find(x => x.id == lv);
                    if (found) {
                        const find = found.child.find(x => x.fid == slt);
                        //An khi chon VeSinhCongCong
                        if (find.lid == "11") {
                            if (!($('#filter-ch-wrapper').hasClass('d-none'))) {
                                $('#filter-ch-wrapper').addClass('d-none')
                            }

                            if (!($('#filter-cx-wrapper').hasClass('d-none'))) {
                                $('#filter-cx-wrapper').addClass('d-none')
                            }

                            if (!($('#filter-td-wrapper').hasClass('col-12'))) {
                                $('#filter-td-wrapper').removeClass('col-lg-4')
                                $('#filter-td-wrapper').addClass('col-12')
                            }

                            $('#filter-ddl-ch-slt').val(-1).trigger('change');
                            $('#filter-ddl-cx-slt').val(-1).trigger('change');
                        } else {
                            if (($('#filter-ch-wrapper').hasClass('d-none'))) {
                                $('#filter-ch-wrapper').removeClass('d-none')
                            }

                            if (($('#filter-cx-wrapper').hasClass('d-none'))) {
                                $('#filter-cx-wrapper').removeClass('d-none')
                            }

                            if (($('#filter-td-wrapper').hasClass('col-12'))) {
                                $('#filter-td-wrapper').removeClass('col-12')
                                $('#filter-td-wrapper').addClass('col-lg-4')
                            }
                        }

                        if (find.lid == "00") {
                            $.ajax({
                                type: 'get',
                                async: false,
                                url: '/api/LoaiHinhCoSoLuuTru',
                                success: function (data) {
                                    let dt = [];
                                    data.forEach(ele => {
                                        let obj = new Object();
                                        obj.id = ele.id;
                                        obj.ten = ele.tenLoai;
                                        dt.push(obj)
                                    });

                                    renderButtonFilter("filter-pl-content", dt)
                                },
                                error: function (err) {
                                    alert('Lỗi.');
                                }
                            });

                            $.ajax({
                                type: 'get',
                                async: false,
                                url: '/api/BanDo/tn/1',
                                success: function (data) {
                                    renderButtonFilter("filter-cslt-dv-content", data)
                                },
                                error: function (err) {
                                    alert('Lỗi.');
                                }
                            });

                            $.ajax({
                                type: 'get',
                                async: false,
                                url: '/api/BanDo/lp',
                                success: function (data) {
                                    renderButtonFilter("filter-cslt-lp-content", data)
                                },
                                error: function (err) {
                                    alert('Lỗi.');
                                }
                            });

                            if (($('#filter-pl-wrapper').hasClass('d-none'))) {
                                $('#filter-pl-wrapper').removeClass('d-none')
                            }

                            if (($('#filter-cslt-wrapper').hasClass('d-none'))) {
                                $('#filter-cslt-wrapper').removeClass('d-none')
                            }
                        }
                        else {
                            if (!($('#filter-pl-wrapper').hasClass('d-none'))) {
                                $('#filter-pl-wrapper').addClass('d-none')
                            }

                            if (!($('#filter-cslt-wrapper').hasClass('d-none'))) {
                                $('#filter-cslt-wrapper').addClass('d-none')
                            }
                        }
                    }
                })

                view.when(() => {
                    //Array position chi duong nhieu diem
                    let arrPos = [];

                    const searchWidget = new Search({
                        view: view
                    });

                    // Add the search widget to the top right corner of the view
                    view.ui.add(searchWidget, {
                        position: "top-right"
                    });

                    view.ui.move("zoom", "top-right");

                    const basemapGallery = new BasemapGallery({
                        view: view,
                        container: document.createElement("div"),
                        source: [Basemap.fromId("arcgis-topographic"), Basemap.fromId("satellite"), baseMap]
                    });

                    const bgExpand = new Expand({
                        view: view,
                        content: basemapGallery
                    });

                    view.ui.add(bgExpand, "bottom-left");

                    //const measurement = new Measurement({
                    //    view: view,
                    //    activeTool: "distance"
                    //});

                    //view.ui.add(measurement, "top-left");

                    //Layer toggle btn
                    var element = document.createElement("div");
                    element.className = "esri-icon-layers esri-widget--button esri-widget esri-interactive";
                    element.addEventListener("click", function (evt) {
                        $('#map-slider').toggleClass('toggle-display');

                        if (!($('#filter-slider').hasClass('toggle-display'))) {
                            $('#filter-slider').addClass('toggle-display')
                        }

                        if (!($('#result-slider').hasClass('toggle-display'))) {
                            $('#result-slider').addClass('toggle-display')
                        }
                    });
                    view.ui.add(element, "top-right");

                    //Filter toggle btn
                    var eleFilter = document.createElement("div");
                    eleFilter.className = "esri-icon-search esri-widget--button esri-widget esri-interactive";
                    eleFilter.addEventListener("click", function (evt) {
                        $('#filter-slider').toggleClass('toggle-display');

                        if (!($('#map-slider').hasClass('toggle-display'))) {
                            $('#map-slider').addClass('toggle-display')
                        }

                        if (!($('#result-slider').hasClass('toggle-display'))) {
                            $('#result-slider').addClass('toggle-display')
                        }
                    });
                    view.ui.add(eleFilter, "top-right");

                    //Clear router btn
                    const btnClearRoute = document.createElement('div');
                    btnClearRoute.className =
                        "esri-icon-erase esri-widget--button esri-widget esri-interactive";
                    btnClearRoute.addEventListener("click", function (evt) {
                        if (graphicsLayer && polylineGraphic) {
                            graphicsLayer.remove(polylineGraphic);
                            routerPopup.visible = false;
                        }

                        arrPos = []
                    });
                    view.ui.add(btnClearRoute, "top-right");

                    //Locate Btn
                    const locateBtn = new Locate({
                        view: view
                    });

                    view.ui.add(locateBtn, {
                        position: "top-left"
                    });

                    let routerContent = '';

                    const escapeRegExpMatch = function (s) {
                        return s.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&');
                    };

                    const isExactMatch = (str, match) => {
                        return new RegExp(`\\b${escapeRegExpMatch(match)}\\b`,'i').test(str)
                    }

                    let routerFlag = false;

                    view.popup.on("trigger-action", (evt) => {
                        if (evt.action.id === "routes") {
                            const mPopup = view.popup;
                            let mLat = null;
                            let mLong = null;

                            if (routerFlag == false) {
                                //Click tren map
                                if (mPopup.selectedFeature.geometry.latitude && mPopup.selectedFeature.geometry.longitude) {
                                    routerFlag = true;

                                    mLat = mPopup.selectedFeature.geometry.latitude;
                                    mLong = mPopup.selectedFeature.geometry.longitude;

                                    const item = arrPos.find(x => (x[0] == mLong && x[1] == mLat));
                                    if (!item) {
                                        if (polylineGraphic) {
                                            graphicsLayer.remove(polylineGraphic);
                                        }

                                        const des = [mLong, mLat];

                                        locateBtn.locate()
                                            .then(function (pos) {
                                                const { latitude: curLat, longitude: curLong } = pos.coords

                                                const curr = [curLong, curLat];

                                                if (arrPos.length == 0) {
                                                    arrPos.push(curr)
                                                }

                                                arrPos.push(des);

                                                var myHeaders = new Headers();
                                                myHeaders.append("Authorization", "5b3ce3597851110001cf624851a99071ab2944ee85a0f5e9f5e40c36");
                                                myHeaders.append("Content-Type", "application/json");

                                                var raw = JSON.stringify({
                                                    "coordinates": arrPos,
                                                    "continue_straight": "true"
                                                });

                                                var requestOptions = {
                                                    method: 'POST',
                                                    headers: myHeaders,
                                                    body: raw,
                                                    redirect: 'follow'
                                                };

                                                fetch("https://api.openrouteservice.org/v2/directions/driving-car/geojson", requestOptions)
                                                    .then(res => {
                                                        return res.json()
                                                    })
                                                    .then(data => {
                                                        routerContent = '';

                                                        const { features } = data;
                                                        const [resultFeature] = features;
                                                        const { geometry, properties } = resultFeature;
                                                        const { segments } = properties;
                                                        const [segmentResult] = segments
                                                        const { steps } = segmentResult
                                                        const { coordinates } = geometry;

                                                        segments.forEach((ele) => {
                                                            ele.steps.forEach((el) => {
                                                                if (el && el.instruction != null) {
                                                                    //Chi duong hien tai
                                                                    let str = el.instruction;

                                                                    for (let i = 0; i < dataDirection.length; i++) {
                                                                        let e = dataDirection[i];

                                                                        const check = isExactMatch(str, e.en);

                                                                        if (check) {
                                                                            const regex = new RegExp(`\\b${escapeRegExpMatch(e.en)}\\b`, 'i');

                                                                            let ar = str.replace(regex, e.vi);

                                                                            str = ar;
                                                                        }
                                                                    }

                                                                    routerContent += `<p class="router-hd">${str}</p>`;
                                                                }
                                                            })
                                                        })

                                                        polylineGraphic = new Graphic({
                                                            geometry: new Polyline({
                                                                hasZ: false,
                                                                hasM: true,
                                                                paths: [...coordinates],
                                                            }),
                                                            symbol: {
                                                                type: "simple-line",
                                                                color: [226, 119, 40],
                                                                width: 3
                                                            }
                                                        });

                                                        graphicsLayer.add(polylineGraphic);

                                                        routerPopup.content = routerContent;
                                                        routerPopup.visible = true;

                                                        routerFlag = false;
                                                    })
                                                    .catch(er => {
                                                        console.log(er)
                                                        routerFlag = false;
                                                    })
                                            })
                                            .catch((error) => {
                                                routerFlag = false;
                                                alert('Cần cấp quyền cần thiết để sử dụng chức năng này!');
                                            })
                                    } else {
                                        routerFlag = false;
                                    }
                                }
                                //Click list
                                else {
                                    routerFlag = true;

                                    const point = new Point(
                                        mPopup.selectedFeature.geometry.x,
                                        mPopup.selectedFeature.geometry.y,
                                        new SpatialReference({
                                            wkt: MAP_SR,
                                        })
                                    );

                                    let outSpatialReference = new SpatialReference({ wkid: 3405 });

                                    projection.load()
                                        .then(function () {
                                            const res = projection.project(point, outSpatialReference);

                                            const point1 = new Point(
                                                res.x,
                                                res.y,
                                                new SpatialReference({
                                                    wkid: 3405
                                                })
                                            );

                                            let outSpatialReference1 = new SpatialReference({ wkid: 4326 });

                                            const res1 = projection.project(point1, outSpatialReference1);

                                            if (res1 != null && res1.longitude != null && res1.latitude != null) {
                                                mLat = res1.latitude;
                                                mLong = res1.longitude;

                                                const item = arrPos.find(x => (x[0] == mLong && x[1] == mLat));
                                                if (!item) {
                                                    const des = [mLong, mLat];

                                                    if (polylineGraphic) {
                                                        graphicsLayer.remove(polylineGraphic);
                                                    }

                                                    locateBtn.locate()
                                                        .then(function (pos) {
                                                            const { latitude: curLat, longitude: curLong } = pos.coords

                                                            const curr = [curLong, curLat];

                                                            if (arrPos.length == 0) {
                                                                arrPos.push(curr)
                                                            }

                                                            arrPos.push(des);

                                                            var myHeaders = new Headers();
                                                            myHeaders.append("Authorization", "5b3ce3597851110001cf624851a99071ab2944ee85a0f5e9f5e40c36");
                                                            myHeaders.append("Content-Type", "application/json");

                                                            var raw = JSON.stringify({
                                                                "coordinates": arrPos,
                                                                "continue_straight": "true"
                                                            });

                                                            var requestOptions = {
                                                                method: 'POST',
                                                                headers: myHeaders,
                                                                body: raw,
                                                                redirect: 'follow'
                                                            };

                                                            fetch("https://api.openrouteservice.org/v2/directions/driving-car/geojson", requestOptions)
                                                                .then(res => {
                                                                    return res.json()
                                                                })
                                                                .then(data => {
                                                                    routerContent = '';

                                                                    const { features } = data;
                                                                    const [resultFeature] = features;
                                                                    const { geometry, properties } = resultFeature;
                                                                    const { segments } = properties;
                                                                    const [segmentResult] = segments
                                                                    const { steps } = segmentResult
                                                                    const { coordinates } = geometry;

                                                                    segments.forEach((ele) => {
                                                                        ele.steps.forEach((el) => {
                                                                            if (el && el.instruction != null) {
                                                                                //Chi duong hien tai
                                                                                let str = el.instruction;

                                                                                for (let i = 0; i < dataDirection.length; i++) {
                                                                                    let e = dataDirection[i];

                                                                                    const check = isExactMatch(str, e.en);

                                                                                    if (check) {
                                                                                        const regex = new RegExp(`\\b${escapeRegExpMatch(e.en)}\\b`, 'i')

                                                                                        let ar = str.replace(regex, e.vi);

                                                                                        str = ar;
                                                                                    }
                                                                                }

                                                                                routerContent += `<p class="router-hd">${str}</p>`;
                                                                            }
                                                                        })
                                                                    })

                                                                    polylineGraphic = new Graphic({
                                                                        geometry: new Polyline({
                                                                            hasZ: false,
                                                                            hasM: true,
                                                                            paths: [...coordinates],
                                                                        }),
                                                                        symbol: {
                                                                            type: "simple-line",
                                                                            color: [226, 119, 40],
                                                                            width: 3
                                                                        }
                                                                    });

                                                                    graphicsLayer.add(polylineGraphic);

                                                                    routerPopup.content = routerContent;
                                                                    routerPopup.visible = true;

                                                                    routerFlag = false;
                                                                })
                                                                .catch(er => {
                                                                    console.log(er)
                                                                    routerFlag = false;
                                                                })
                                                        })
                                                        .catch((error) => {
                                                            routerFlag = false;
                                                            alert('Cần cấp quyền cần thiết để sử dụng chức năng này!');
                                                        })
                                                } else {
                                                    routerFlag = false;
                                                }
                                            }
                                            else {
                                                routerFlag = false;
                                                alert('Đã xảy ra lỗi!');
                                            }
                                        })
                                        .catch((error) => {
                                            routerFlag = false;
                                            alert('Đã xảy ra lỗi!');
                                        })
                                }
                            }
                        }
                    });

                    //Tim kiem nang cao
                    $('#filter-type-slt').on('change', function () {
                        const slt = $(this).val();
                        if (slt == "nc") {
                            if ($('#adv-filter-content').hasClass('d-none')) {
                                $('#adv-filter-content').removeClass('d-none')
                            }

                            if (!($('#adv-filter-xq-content').hasClass('d-none'))) {
                                $('#adv-filter-xq-content').addClass('d-none')
                            }
                        }
                        else if (slt == "xq") {
                            if (!($('#adv-filter-content').hasClass('d-none'))) {
                                $('#adv-filter-content').addClass('d-none')
                            }

                            if ($('#adv-filter-xq-content').hasClass('d-none')) {
                                $('#adv-filter-xq-content').removeClass('d-none')
                            }
                        }
                    });

                    let resultArr = [];

                    $('#filter-btn').on('click', async function () {
                        if (!($('#filter-btn').hasClass('disabled'))) {
                            const type = $('#filter-type-slt').val();

                            let fid = $('#filter-lh-slt').val();
                            if (fid == null) {
                                fid = $('#filter-lv-slt').val();
                            }

                            if (fid) {
                                let lid = null;
                                let tid = null;
                                groupData.forEach((el) => {
                                    if (el.fid == fid) {
                                        lid = el.lid;
                                    } else {
                                        el.child.forEach((e) => {
                                            if (e.fid == fid) {
                                                lid = e.lid;
                                                tid = e.tid;
                                            }
                                        })
                                    }
                                })
                                if (lid) {
                                    const layer = map.layers.items.find(x => (x.id == lid && x.type === 'feature'));
                                    if (layer) {
                                        currentLayerSearch = layer.id;
                                        layer.visible = true;

                                        if (tid) {
                                            setTreeCheck(tid)
                                        } else {
                                            setTreeCheck(lid)
                                        }

                                        $('#filter-btn').addClass('disabled')
                                        resultArr = [];
                                        if (type == "xq") {
                                            //Tim kiem xung quanh
                                            locateBtn.locate()
                                                .then(async function (pos) {
                                                    if (pos && pos.coords && pos.coords.latitude && pos.coords.longitude) {
                                                        const point = new Point(
                                                            pos.coords.longitude,
                                                            pos.coords.latitude
                                                        );

                                                        let outSpatialReference = new SpatialReference({ wkid: 3405 });

                                                        projection.load()
                                                            .then(async function () {
                                                                const res = projection.project(point, outSpatialReference);

                                                                const point1 = new Point(
                                                                    res.x,
                                                                    res.y,
                                                                    new SpatialReference({
                                                                        wkid: 3405
                                                                    })
                                                                );

                                                                let outSpatialReference1 = new SpatialReference({ wkt: MAP_SR });

                                                                const res1 = projection.project(point1, outSpatialReference1);

                                                                const point2 = new Point(
                                                                    res1.x,
                                                                    res1.y,
                                                                    new SpatialReference({
                                                                        wkt: MAP_SR
                                                                    })
                                                                );

                                                                let radius = $('#bk-input').val();
                                                                if (radius < 5) {
                                                                    radius = 5;
                                                                } else if (radius > 5000) {
                                                                    radius = 5000;
                                                                }

                                                                let query = null;
                                                                if (tid) {
                                                                    const field = layer.sourceJSON.typeIdField;
                                                                    if (field != null && field != "") {
                                                                        query = `${field} = '${tid}'`
                                                                    }
                                                                }

                                                                const rad = Number(radius) + 100;

                                                                let arr = [];
                                                                arr.push(...(await getDataXqWithQuery(layer, query, point2, Number(radius))));

                                                                $('#filter-slider').toggleClass('toggle-display');
                                                                $('#result-slider').toggleClass('toggle-display');

                                                                $('#filter-btn').removeClass('disabled')

                                                                if (arr.length > 0) {
                                                                    resultArr = arr;
                                                                    renderResult(resultArr)
                                                                } else {
                                                                    renderResult(null)
                                                                }
                                                            })
                                                            .catch((error) => {
                                                                routerFlag = false;
                                                                alert('Đã xảy ra lỗi!');
                                                            })
                                                    }
                                                });
                                        }
                                        else if (type == "nc") {
                                            //Tim kiem nang cao
                                            const query = buildQuery(layer, tid);

                                            if (layer.id == "00") {
                                                //CSLT - PhanLoai
                                                let plArr = [];

                                                $('#filter-pl-content .filter-selector.selected').each(function () {
                                                    const slt = $(this).data('fid');
                                                    plArr.push(slt)
                                                });

                                                //CSLT - LoaiPhong
                                                let lpArr = [];

                                                $('#filter-cslt-lp-content .filter-selector.selected').each(function () {
                                                    const slt = $(this).data('fid');
                                                    lpArr.push(slt)
                                                });

                                                //CSLT - TienNghi
                                                let dvArr = [];

                                                $('#filter-cslt-dv-content .filter-selector.selected').each(function () {
                                                    const slt = $(this).data('fid');
                                                    dvArr.push(slt)
                                                });

                                                //CSLT - HangSao
                                                let hsArr = [];

                                                $('#filter-cslt-hs-content .filter-selector.selected').each(function () {
                                                    const slt = $(this).data('fid');
                                                    hsArr.push(slt)
                                                });

                                                const linhVuc = linhVucData.find(x => x.id == layer.id);

                                                if (linhVuc) {
                                                    const data = {
                                                        "linhVucId": linhVuc.linhVucId,
                                                        "hangSao": hsArr.length > 0 ? hsArr.join() : null,
                                                        "loaiHinhId": plArr.length > 0 ? plArr.join() : null,
                                                        "loaiDiaDiemAnUong": null,
                                                        "tienNghi": dvArr.length > 0 ? dvArr.join() : null,
                                                        "loaiPhong": lpArr.length > 0 ? lpArr.join() : null,
                                                    };

                                                    var myHeaders = new Headers();
                                                    myHeaders.append("Content-Type", "application/json");

                                                    var raw = JSON.stringify(data);

                                                    var requestOptions = {
                                                        method: 'POST',
                                                        headers: myHeaders,
                                                        body: raw,
                                                        redirect: 'follow'
                                                    };

                                                    fetch("/api/BanDo/hs", requestOptions)
                                                        .then(response => response.json())
                                                        .then(async function (data) {
                                                            let arr = [];
                                                            arr.push(...(await getDataWithQueryFromDB(layer, data, query)));

                                                            $('#filter-slider').toggleClass('toggle-display');
                                                            $('#result-slider').toggleClass('toggle-display');

                                                            $('#filter-btn').removeClass('disabled')

                                                            if (arr.length > 0) {
                                                                resultArr = arr;
                                                                renderResult(resultArr)
                                                            } else {
                                                                renderResult(null)
                                                            }
                                                        })
                                                        .catch(error => console.log('error', error));

                                                }
                                            }
                                            else {
                                                let arr = [];
                                                arr.push(...(await getDataWithQuery(layer, query)));

                                                $('#filter-slider').toggleClass('toggle-display');
                                                $('#result-slider').toggleClass('toggle-display');

                                                $('#filter-btn').removeClass('disabled')

                                                if (arr.length > 0) {
                                                    resultArr = arr;
                                                    renderResult(resultArr)
                                                } else {
                                                    renderResult(null)
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        alert('Không tìm thấy lớp dữ liệu tương ứng!')
                                    }
                                }
                                else {
                                    alert('Không tìm thấy lớp dữ liệu tương ứng!')
                                }
                            }
                        }
                    });

                    $('#filter-cancel-btn').on('click', function () {
                        $('#filter-slider').toggleClass('toggle-display');
                    });

                    $(document).on('click', '.filter-selector', function () {
                        $(this).toggleClass('selected');
                    })

                    $(document).on('click', '.result-list-item', function () {
                        const id = $(this).data('oid');
                        let found = resultArr.find(x => x.attributes.objectid == id);
                        if (currentLayerSearch == 13) {
                            found = resultArr.find(x => x.attributes.objectid_1 == id);
                        }
                        if (found) {
                            const point = new Point(
                                found.geometry.x,
                                found.geometry.y,
                                new SpatialReference({
                                    wkt: MAP_SR,
                                })
                            );

                            let outSpatialReference = new SpatialReference({ wkid: 3405 });

                            projection.load().then(function () {
                                const res = projection.project(point, outSpatialReference);

                                const point1 = new Point(
                                    res.x,
                                    res.y,
                                    new SpatialReference({
                                        wkid: 3405
                                    })
                                );

                                let outSpatialReference1 = new SpatialReference({ wkid: 4326 });

                                const res1 = projection.project(point1, outSpatialReference1);

                                if (res1 != null && res1.longitude != null && res1.latitude != null) {
                                    view.goTo({
                                        center: [res1.longitude, res1.latitude],
                                        zoom: 18
                                    });

                                    view.popup.open({
                                        features: [found],
                                        location: found.geometry
                                    });
                                }
                            });
                        }
                    })
                });

                async function getFeatureById(layer, arr) {
                    const queryObj = new Query();
                    queryObj.outFields = ["*"];
                    queryObj.returnGeometry = true;
                    queryObj.objectIds = arr;

                    const res = await layer.queryFeatures(queryObj);

                    return res.features;
                };

                async function getFeatureByDbIdWithQuery(layer, arr, query) {
                    const list = returnQuery(arr);

                    query += ` AND (id IN (${list}))`;

                    const queryObj = new Query();
                    queryObj.where = query;
                    queryObj.outFields = ["*"];
                    queryObj.returnGeometry = true;

                    const res = await layer.queryFeatures(queryObj);

                    return res.features;
                };

                const MAX_QUERY = 200;

                //Query truong hop tren 1000
                async function getDataWithQuery(layer, clause) {
                    let arr = [];

                    const queryId = new Query();
                    queryId.where = clause;

                    const results = await layer.queryObjectIds(queryId);

                    if (results != null) {
                        const page = Math.ceil(results.length / MAX_QUERY);

                        for (let i = 0; i < page; i++) {
                            let start = i * MAX_QUERY;
                            let end = (i + 1) * MAX_QUERY;
                            var items = results.slice(start, end);

                            arr.push(...(await getFeatureById(layer, items)));
                        }
                    }

                    return Promise.all(arr);
                };

                //Query xung quanh truong hop tren 1000
                async function getDataXqWithQuery(layer, clause, point, radius) {
                    let arr = [];

                    const queryId = new Query();
                    queryId.where = clause;
                    queryId.geometry = point;
                    queryId.distance = radius;
                    queryId.units = "meters";
                    queryId.spatialRelationship = "intersects";

                    const results = await layer.queryObjectIds(queryId);

                    if (results != null) {
                        const page = Math.ceil(results.length / MAX_QUERY);

                        for (let i = 0; i < page; i++) {
                            let start = i * MAX_QUERY;
                            let end = (i + 1) * MAX_QUERY;
                            var items = results.slice(start, end);

                            arr.push(...(await getFeatureById(layer, items)));
                        }
                    }

                    return Promise.all(arr);
                };

                //Query truong hop tren 1000 voi id query tu DB
                async function getDataWithQueryFromDB(layer, results, clause) {
                    let arr = [];
                    if (results != null && results.length > 0) {
                        const page = Math.ceil(results.length / MAX_QUERY);

                        for (let i = 0; i < page; i++) {
                            let start = i * MAX_QUERY;
                            let end = (i + 1) * MAX_QUERY;
                            var items = results.slice(start, end);

                            arr.push(...(await getFeatureByDbIdWithQuery(layer, items, clause)));
                        }
                    }

                    return Promise.all(arr);
                };

                function renderResult(data) {
                    $('#result-content').empty();
                    let html = '';
                    if (data != null) {
                        data.forEach((el) => {
                            let ten = el.attributes.ten ? el.attributes.ten : el.attributes.tendiadiem ? el.attributes.tendiadiem : el.attributes.tenlehoi ? el.attributes.tenlehoi : el.attributes.tieude ? el.attributes.tieude : null;
                            let diachi = el.attributes.sonha ? el.attributes.sonha : el.attributes.diachi ? el.attributes.diachi : el.attributes.vitri ? el.attributes.vitri : el.attributes.diadiem ? el.attributes.diadiem : el.attributes.diachisuki ? el.attributes.diachisuki : null;
                            let id = el.attributes.objectid_1 ? el.attributes.objectid_1 : el.attributes.objectid;
                            html += `
                            <div class="col-12 result-list-item" data-oid="${id}">
						        <h4>${ten}</h4>
						        <p>${diachi}</p>
					        </div>
                        `
                        });
                    } else {
                        html += `
                                <div class="col-12">
                                    <p>Không tìm thấy dữ liệu tương ứng</p>
                                </div>
                                `
                    }
                    $('#result-content').append(html);
                };

                function renderButtonFilter(loc, data) {
                    $(`#${loc}`).empty();
                    let html = '';
                    if (data && data.length > 0) {
                        data.forEach((el) => {
                            html += `
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3 btn-filter-item">
                                        <button type="button" class="btn filter-selector m-custom-col" data-fid="${el.id}">
                                            ${el.ten}
                                        </button>
                                    </div>
                                    `
                        });
                    } else {
                        return false;
                    }
                    $(`#${loc}`).append(html);
                };

                function buildQuery(layer, tid) {
                    let query = '1=1';

                    const keyword = $('#search-input').val();
                    
                    if (tid) {
                        const field = layer.sourceJSON.typeIdField;
                        if (field != null && field != "") {
                            query = `${field} = '${tid}'`
                        }
                    }

                    let check = checkEmptyBlank(keyword);
                    if (check == false) {
                        const kw = keyword.toLowerCase();
                        if (tid) {
                            if (layer.id == 10) {
                                query += ` AND (lower(tendiadiem) LIKE '%${kw}%' OR dienthoai LIKE '%${kw}%')`;
                            } else if (layer.id == 11) {
                                query += ` AND (lower(ten) LIKE '%${kw}%' OR lower(mota) LIKE '%${kw}%' OR lower(hientrang) LIKE '%${kw}%')`;
                            } else if (layer.id == 12) {
                                query += ` AND (lower(tenlehoi) LIKE '%${kw}%' OR lower(noidung) LIKE '%${kw}%')`;
                            } else if (layer.id == 13) {
                                query += ` AND (lower(tieude) LIKE '%${kw}%' OR lower(tencoquan) LIKE '%${kw}%')`;
                            } else {
                                query += ` AND (lower(ten) LIKE '%${kw}%' OR sodienthoa LIKE '%${kw}%')`;
                            }
                        } else {
                            if (layer.id == 10) {
                                query = `(lower(tendiadiem) LIKE '%${kw}%' OR dienthoai LIKE '%${kw}%')`;
                            } else if (layer.id == 11) {
                                query = `(lower(ten) LIKE '%${kw}%' OR lower(mota) LIKE '%${kw}%' OR lower(hientrang) LIKE '%${kw}%')`;
                            } else if (layer.id == 12) {
                                query = `(lower(tenlehoi) LIKE '%${kw}%' OR lower(noidung) LIKE '%${kw}%')`;
                            } else if (layer.id == 13) {
                                query = `(lower(tieude) LIKE '%${kw}%' OR lower(tencoquan) LIKE '%${kw}%')`;
                            } else {
                                query = `(lower(ten) LIKE '%${kw}%' OR sodienthoa LIKE '%${kw}%')`;
                            }
                        }
                    }

                    const ch = $('#filter-ddl-ch-slt').val();
                    const cx = $('#filter-ddl-cx-slt').val();
                    const td = $('#filter-ddl-duong-input').val();

                    if (layer.id != 11 && layer.id != 13) {
                        if (ch && ch != -1) {
                            query += ` AND (quanhuyeni = '${ch}')`
                        }

                        if (cx && cx != -1) {
                            query += ` AND (phuongxaid = '${cx}')`
                        }
                    }

                    let checktd = checkEmptyBlank(td);
                    if (checktd == false) {
                        const ten = td.toLowerCase();
                        if (layer.id == 10) {
                            query += ` AND (lower(diachi) LIKE '%${ten}%')`;
                        } else if (layer.id == 11) {
                            query += ` AND (lower(vitri) LIKE '%${ten}%')`;
                        } else if (layer.id == 12) {
                            query += ` AND (lower(diadiem) LIKE '%${ten}%')`;
                        } else if (layer.id == 13) {
                            query += ` AND (lower(diachisuki) LIKE '%${ten}%')`;
                        } else {
                            query += ` AND (lower(sonha) LIKE '%${ten}%')`;
                        }
                    }

                    return query;
                }

                const data = [
                    {
                        id: "-1",
                        text: "Điểm du lịch",
                        children: [
                            {
                                id: "561",
                                text: "Văn hóa",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA4AAAAOCAYAAAAfSC3RAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAZ5JREFUKJGd0k8og3Ecx/HPs82ePds8Y0bmz1hLcnDiojgo4oKLo6O4LynbwV2SOIjkJkrKwd+lppUcUDaWfys024+0p3lmPE8eexy0tbFGfrffr16/3r9fXxX+uVTpm/tRmZFiGHiV4r3x91glTTFPrMaw/iFh2jZFcVnhnV22Bh+Cbl6IVmnz8pGvZvEqxapIJNioVtD2m2G50zpGHWTAQL9MX3Pne4XaIkttcX3avUaUogJx4YW9CHldt3a5rnqCuk/BG+ncydIGS5G2JOt7dBo9LIU2/dnj8QyArhSMC3x3jaku52foNHooFMrmjFSaZgpyqmQ4YzLUoqP8EjshFQAkpA/+L5BWMs+82WcEwRfkhIgXQP0vDo8v4WNCSCiVuuydc7dY21oBlOdwwu7V2hYALgU3AivbTUezbF/j4AiAbF/75iVHjoXryf3kQXIAwhM+x/5tNDDkbB9roGSqB0AZgAgSsmv1dHHT4ennRVE8/A7BcdzhPDcemj8ZlwB4ACQAUADUZrPZL4qiPz0hY1YBhAEsfe8khPxo/wSHrpiVEKKbiwAAAABJRU5ErkJggg=="
                            },
                            {
                                id: "562",
                                text: "Làng nghề truyền thống",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA4AAAAOCAYAAAAfSC3RAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAZJJREFUKJGd0r9LAmEYB/Dvq9Z5cXllJtgvEScHpxxbAqOWaulPkNolgmxoFwmpIXKXBheHqHQxgmhIIY0Qys30ivAwz+iOzt6G8lATi57tfeDz8n0eHgP+WYbWB6XbLCCtvr6qK5L0Psmy5JnnjUeAukfIrtgVUup3FIuPqWpVtg8O9sFk6ockqfZiseJhGJ2f0o0FQoKXbZBSH5PPi2cjIwNTbveo9qvZDExMAPW6bMpmS0lK/S5Cdh40mM+rWzzPTFmtA13n4TgjnM5hLpN52gewqMFaTV5yuSw9l8FxRuj1upm2qCzLDPVU32WxsPz8PMYTCZQMANBoqLW/QJbVv+RyNjMgfMFKRc4CcP8Gy+V6RhCEkhY1EsmmvF7HLIDxHk6Ox+9OAIgajMUKpwcHadPammcTgLULekunhUA4fH/RbDQPoBwI5C4Khep6MDg3TQhdBjAGoELpRzIavTn2+c5riqJcdUKIongVComlUOhaBXAO4AMAAdBvs9luFUW5bY3QdqsAygAOO3MKgvAj+yclNZe2XCG8gAAAAABJRU5ErkJggg=="
                            },
                            {
                                id: "563",
                                text: "Thiên nhiên",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA4AAAAOCAYAAAAfSC3RAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAXtJREFUKJGd0r1rU2EUx/Fvk5t7Y0jSmoB4ezUh4FARpwouUnDSRbtI6VIn6R8gQqkuzq4dJOCiDpXMVWOWFCEIFkEb0kkUQm4eY+0jeTVPjDcdzAuJIZae7Qyfw+8cjsYxSxvqlgsncLtWaTVu4dTP4vLuowe2+ONs8OKMHA9X7Bi1b2la5SiaH3Q/tOtRGsVLuPS7xop9XT233g3DOz8NStltvCcj+OYGY3XAdxp+N4Lqx16KJfs8CavQh0Yp+0DpgQh6ePxCHh8EYn6q2cfAjT5UzepNgucmX8PjA9xXhqNq3pnJqlve8DRcs+CN/Rc6TuVIUDPKprkbEoIuVPITcPG/sFb6IISwB1F3n6WxFq5Cx5rAmnxJvgbkAMqXyVDuaVBeuL0OnBqDfoW+f7zP/pNM7wt6D1Ak/yhD7es94/LDeQWL0JkFDoAU+cSr+vu1ilJqhxGIlHIHGbfV53gbeAs4wBSgm6aZE0rlhu40EqkIbI7mFEL8k/0QpECBfUdh2eUAAAAASUVORK5CYII="
                            },
                            {
                                id: "564",
                                text: "Tâm linh",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA4AAAAOCAYAAAAfSC3RAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAX1JREFUKJGd0s8rw3Ecx/Hnl+Xbt7721fyomd8OctgJF1yU4uLHwZ8gjkou+A+0g3IQ5Sg1BzvIr4sl2w44UGuJkti+SfvGZuxT7OvAvhlr5H37HB7vz+vz6WXjn2P7ejA5VKBoLEVqJEm6VkG+11A3IbMg0W3khSahxhvu9h9I1peiYEclyXP9DXftMiWTphzol0RXKAeaBOUIV/5ytDo3TdZWB6XUUMkTL/ZTcbFnEmyV6Ly1YES+mtWEWldFWd73qCg0U6OeEFkEBiyYEKnBVhoKfoaKQjHF3TlRFeT8V32bCjStjxbXLudRG8AbmcRfoIL8eOZMOND5gHEeTwH3bzBG/ETX9agVdRnffi8dPYCrgEv78G8DhgXX8e8sOXz2cWN4GqjKg16OHZGZeTYCfNYgW4DYDCuBS66n5uSJNkkwBFQDcRP2VtnaGk15EkKIo+wmqzmGYRx58EY9wvsKHAAZQAJKnE5nWOgi/DVCTleBGLD2Paeu6z+yvwMUSn+SkgArhQAAAABJRU5ErkJggg=="
                            },
                            {
                                id: "565",
                                text: "Lịch sử",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA4AAAAOCAYAAAAfSC3RAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAYZJREFUKJGd0r9LG2Ecx/H3aZLjYszJVYQzbULpUDLooqNLoaCL7SKIjqXYvbg0/gfFodChtM4SiggRxBoR/IWWEjNEkCCIhGryDNWrXgzNA02uQ9rT2JAWv9szvL58nuf5eLjleK4fnAQakomSZKRY5p7m5ZvuZ5EKb5VxrIbQiXP/WLB2fkmkXYOgH4plIsen9KteXjof1SFlVH6ug848ajbH+h2dcE/4aqvRBncNuJQEMzm54sSJKmOcuDD7VZ3SAzLc1d74PgEVHpgE0jneAcMutEvySbS7+WMEVGhtYaAuquajozmrTWcQffAhoeQBeQ9ApYr9P1DzcrFnmwaIGjwrkgF6/gUL30kLIfJu1A+rrD3u5REQauLKiR0+Qe0/PQBzX1h+v2wEXwxZr4CuBujH7pERe7PB9m/nFqAQW2D7sMDk62dqn4J8CnQDZw6szG6x9HymZEspU382uc2xLCs1nSQ/nZQ/gU2gCiiAzzTNfSnF/vUIdV0FCkD8Zk4hxF/ZfwGk0oDHzNMkcQAAAABJRU5ErkJggg=="
                            },
                        ],
                    },
                    {
                        id: "-2",
                        text: "Du lịch",
                        children: [
                            {
                                id: "00",
                                text: "Lưu trú",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAT9JREFUOI211D8vQ1EYx/Fvn570kCLSkcUb8BashiY2iZGYjNKOFoNEIukbMOmAkYhBxGDrINHNYCEGRHMnwklun2NwcWlv9c/1m85JnvPJc56cHEPKMZ+Lol2dwZlp8JleEYFQC5nacbBV/wKNyy4qfqnfziTwJeAbVMj1i8XPm78Ke82/g2+AB4ZTAQU2QB8U2U4FjOUWtA4yB/T0jNqCUtCFw6BSK1LeEzhXqzctRS47im2u43xJMJsfk0oA9UXmi7ZsBO7DvFyAtBblaUggO2CuIXxN7DCHPXjON+sACrudrqcFOZsMGnePjCdfOSQckaA51QmK54mxq/j+B6hkJkLrl8XJWregWmZxDCV06FfEdUtFcZxEq/024OCJg35Ay/8C9TR6Hj3/h6ChWLnExcAjKlWg2nd/0exTn+E7rDFeJpuwK/sAAAAASUVORK5CYII="
                            },
                            {
                                id: "01",
                                text: "Lữ hành",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAwAAAAUCAYAAAC58NwRAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAOFJREFUKJFjYSARsGATNF7OUMf7h2HSgViGD0RpYGFkqPvOylBovJyhX4iNYcLuYIZPeDVAgQALI0Pjp98MBcYrGHrF/jJM2h7N8BmfBhgQZGFgaHnHzBBmvobBiRgNz5kZGdqPsTDMYghm+IlPw0tmBobOY78ZpjPEMvzA7wdGhoNy/xl8V0cwfEOXwqrhDwPDc2yKcduABwxWDcYrGM6yMDCww0X/M8iYr2C4AuP+YWBYeTaCoRmugYWBQZsBWQME8COZKk2Zk2iv4SQrAz/DNwZGnKokGP6g2hDM8JNYGwCXvzQHMH/37gAAAABJRU5ErkJggg=="
                            },
                        ],
                    },
                    {
                        id: "-3",
                        text: "Dịch vụ",
                        children: [
                            {
                                id: "03",
                                text: "Nhà hàng",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAYBJREFUOI3F1M8rg3EcB/D383yfZyw1ashpSrugGVui1DgoLo5cpjg4qLnJ/yCJG61oMkpxc6BWak+01kqR29pENB5botG29jxfh7XHHltP+1Xep0+fvt/X91d9OdQ5XL4IhsLexHvSQSllKkUIy0rtbQa3rb9rVQHDUdE5u3GtYGYDwYCpCYTkWvexFIJiGgvDrSAEcF/FVejF2tgigF9QkmTVzibtRsxMWcDzHGRZhhCIQic8YWt9GgyAx7l9nEW+lPGyDFZ15HJi6tCD54hSowDMpyKwnGiCg3Yz8vcgBKK1ga9vKeh4ApZlAQBi4rs28PjuE0PbPiy7JuA9usSmLwanraV6EACeX5K53caLL78qsJpUBJ7cfGD8UMC801EfMC1RpNJZzTH/e+SaQUmSAQDZrFzQowCATIZqg0yJT8vtF2E9ELB3/qD0PKcRNDZw8IQS2mCnyXi7u9TX+/c/pJRixdldNHHHZVFqwrC02aD3q8DRkR5rySUrTN0f5QdSz3k+moeUYAAAAABJRU5ErkJggg=="
                            },
                            {
                                id: "02",
                                text: "Mua sắm",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABwAAAAcCAYAAAByDd+UAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAABGdJREFUSInNlltsFFUYx39nZmd3Z9suXehFKLQol1K8BTUkGGMgUWhjS1TQFNAnSQrxyfhUCMtSgpiYxsgLkuDlAbSYmAhFfQNCDdXEcItCwFZCSbuFbgtbht3ZdjrHh51u9zKEEhLiP5lkzpzvnN/5vjPfd46HxyzP/xKYTLZVSzH+rkCskvAcUAZYwADQC3Sijv8U0PZGHwmYBlmfSKxmEKosNFnsPA1MaPsSZvggysSugHfP4EMDE6kdzVJaB4Dgg1adNdcWbHVjIrVzU8C36/i0gUkz/JGUtANimrBsBQejo0e/PNBwbEvLr289EJj2jPaTJ3oEQoATRylzAzrZlNnv0ub2SJKBgbjSvGHZ2kRqeWO+pznAZLKt2gmj2PzBj2z9cAXBEh+qqtB95l/0gM6Kl+chBAgUuk73UFrq56Xl8xGAcW+Mzs7LHDmyEUBBysOJse212XuaA5TC2ouzZ8GZRbz/3jIqnygBwOtV0HUv6995NmNvmmPMqZrB6tWLARjoj3Px4kBOeLHVncDWAqBpRmps7ObJdmkx9PTEsG0bv18jmbTQdW9OWFMpG82jZNrxuElFRQl52pwYb22bTJkM0IFlRhcV+dj3xWnu3BmjqyudXj4fHD50lpr5ISoqApw7208sVsW4ZVNc5KO3N0bZLD0f6GFCexPYnxdSuSr7pywq8hLZVU/d0kpsW/LtN38iJaypX4RhjGMYJjf64mgeldjQPf65Oszvf1xj3dvP5wMBmlyA4plsC69fwzQtABRFoGnpPZw7tzRj88vPl3jt9YW88OI8AIJBLzNm+N2AC6bcnVJZtoVPU1m/7hBvNNXx9NJK/r50kyfnl9LfH2dmKIAe0EiZFppXzYyJRkdZsqTcDTjHDZijz9rXEhsyuDVkMBg1qK4u5cL5KNu3nQJg5crZRKMjlJWXMDKcpKKymKtXbtHUtNRtOk/BCxADqiYboZBOKKSzaHHuig9+bXP7doLh2D1uDRlEBwy6u/s4dz5K59EewpE1bsCYC1D+BaLKzTpnqR6F8vJiysuLWVJXmdO3f383qlpYDQVcdPPwFOC6vEeVRJ4sACqo39vYe3By8a4xxtUrN9MDJFk1NWuarHoKcO3aiBtvQkjthwKg3x+5njDDHcBGgKGbdxkeTnKjL05tbZkDs6cAMg2XwGDUwJqwaaivJRj05QM7dD3cVwAEENLTKoXVCASfWjCLzmOXaW09wbkLW6mtdf3dsSyb+tVfMRxP0f1bC35dy+4eFdKzLftDDlDXw32J1I4WpPgOEAsWzqKhoYZQqKBcZaQqglderSZhpHJyEpAI2aL7p7wrAAIEfLs7kmZ4toT2xqY60dhUd18YgFAEkcJUkAI+1n27O/I7XBNf97d9nkjtiCLFw1wxJjWKkC1usPsCwfE02XbGOSNzTpL7yAY6hPS05odxWkBI7ymwyTQj22wmNgArnSI/WXdjTsE4qaB0+P2R6w9Y1PTupc5EnzrPI+mx37z/A4U7qeHePivMAAAAAElFTkSuQmCC"
                            },
                            {
                                id: "05",
                                text: "Vui chơi giải trí",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAdRJREFUOI2t1D9IVXEUB/DPMyVIQsWyhJyKgoyGsKGapLJCaGloKQgEq8XMJYq2liAMNFvcShBCNBpKDMw/IUKGlJQl5hCUvkoELa0Xcm+DZj59ynvSgQuXc87vc7/3Djfdf670vzfrq8LDsVARIikrgdl8no7VRAYWwFObVTRGnVxrsrHQT/wDj+5xojGaeHl9GkeyeTyxqpnBoldOS5trLK6qXUzHKN3H8YOcq2ZbDi8+0TmVWE1P3OZYNrfOM/0LIS8Hqb1A9kYGR9h7N0Wwc4q3IwQhD7oYmyRnA9fP8HxgpVOrgLFgLsXVQj6M0/SFa4WU19H8NUWw/yJbcwkDqptZF+HjFTLmv/Kd+b3WXsrakwDzcsjfxMMO+kZpqmRLbvzO9xm63yeZEHpecfkJ7RXLsdhvbjRw73OS4OthzrbQVsb2gvhZEFDXwsQP6osp70gCLG2i7TRFu5fPGttoH6Khkq7+JBPeL6HkwPJ+Wy83u2m9RG5WopMrgLlZ9L2J732bnEtee4jo+Nw1PJokWNqU+OlQ0YOelecLYOr/rCUVEcaBBXmG64vDHanSQWD22ZDuR1HvYovB4v2RnarCTLNrCJspUBOZiUsIbkemU8aW1B+WcI7zDGduCQAAAABJRU5ErkJggg=="
                            },
                            {
                                id: "06",
                                text: "Chăm sóc sức khỏe",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAALhJREFUOI3Vkz0KwkAQRt9AIEi67VJZi21KT2FjaeVFxGNYiDfwHGop1lYphO1SDIhrYWN249+SZj/Y5n3MY1iYjJ6TfSodTIFlUJTlTOr69LcQMMA4oKqDqA1jkpDQGVP5pWuaoaiGU9aOnDE+awTO2QvY+3PyfpEt1vrsCFQJ/WESwoVfOpgIzAOe5ytRvXj42hIKrDuE0CGUotiJ6uHbhr0kfeEN6Lg97lFCgQ3P1054dr8JY/IAcTwttQ2OxDMAAAAASUVORK5CYII="
                            },
                            {
                                id: "07",
                                text: "Thể thao",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAo5JREFUWIXt11FIU1Ecx/HvultzlClrbE7UxGZTy4woyEoqcagFvUhikCFFQlgRJNHb3qIHKbCHogIhX3zoxRI1TBvNEqVZ4SpXAzWtxWLLSTqnbfUgLSTb3LzLoH5Ph/899/w/XM653CtlmSP9D/hLAUYFe6fkonUx+Wfh8uTiAPraevyTpxhDIhpAtwKobcJedzg8wM9JELH5z1Sw80wNT+rdoQER7gutEKBqs5eed3GYXELoyVOsXFKzhdJy7DVb09sY9+o5eG0/5nAIsQGZ2rcAJCpsFK3fg9m15s8CrKPZ5OvsTHj1tNpWRXx/VACtEOBCgYd9WWPokgb5BphtefR6Inv8UQGUAjyq6UenMc2rl+Q1U/n0BI1DcbEFNJaNoNOY8H3VIpc6gnVB4qNyh4O+D+s4t9tNdadKfIBeHqBwYzcB5Nx4WMJpQ8O864U5LbzIUjLpU1LdWSo+4EDWNHFSB9b3BjYkfQnW+4dLGRhVUZ7/AEHiw/wmd9FrRgR47pBx71k5TRY11yvbg/URVwJV7Rqu9lQAYIlgM0YE6HLK6LqTRkPxJ+Ll9mA9O9kJpETUOCrAjxTnWQG4P3AIw6a7ZGoeY1Dn0uGUxQZwPNNLhnIWqSxAhmoKbYKFGb8SY1sKqWt3kZPcxdFtbjpaNbEBXCwzoY5/Oa/WN1RAr0dg0KEmJxnyM4eBGAGaLdvZkpaORAIzfilOj4Jb3XPNPo4rAFCudodaYmmA6g4VsPCLRaeeAGDMlRI7QKjkpg0D0G5NXR6AXDaNzVHI+e7E5QEcuVnEqyjOfyjADPz66fS7tEVy9uMFX1iAHC754OzcULTMIgRuY77yOSzAZ68zAkYRm4fM3/pn9A8BvgNmqcBChGYDigAAAABJRU5ErkJggg=="
                            },
                            {
                                id: "08",
                                text: "Vận chuyển",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAhlJREFUOI2tz11IU2Ecx/Hv2c7Z8Y0VLg3UnHkjytKMvJPSlTcSUkQXgSAxFFZhCzLIqyxC66IougoqyyCKbmytm9WghN4syyhEaUtBKWsSw9lePNu6amXnbOnqf/XA7/l/nt8j8p9H/HnovzFkz8/PcYiiLneFRiIaVfxfZuePdNisD5NgPBHva+l5YsywWMn9k/Wngc1JUK/XZYoBIEn6Qvjty+mmv7MG8zoTb97NcPjauOYdAYG0oKwXGOiqo8CUx6aaMozGHCorithoKcY35cd28a3mXkrwyqFaLJVFPBv28XHK/6uJAE0NVfR+DXLslm/54LaGKrp6XAy8+qbKTn0OsmtH9fLBc20VSJKeZms5zVZ1LiBQVlpAp3UtFzyz6cGDjYUcaLfy8rWX3S11SJL6zYXvYd6PTXPm+E7GvNdxT4VSg+biVQQCCzQ63Izf3IO5tFAFTnz4RGu3m9G7NspLciEdCLDGZCTg6UCWJa2Y2ur1jA7akA3qXAUuRmOEw1GysgxEIosoSky1JBlEZFkiFIqgKIn0YPdtH9nZj3DYm7gzOEzr2REV6O7byvaGDRw9Mcjl53PpQQDP0xka6yd58HhSK8Yz5CV/dR7OF3OqTBN0TgRxtt/TxAB6XdP0uqY1sySoxOIRQE6p/GWUWDy4BBRF4eql/ZZ9ok63YlSJxef9/uD5JWDb3i12wJ5pw2SxfwX+nB8WmqlYGmzMmwAAAABJRU5ErkJggg=="
                            },
                            {
                                id: "11",
                                text: "Vệ sinh công cộng",
                                type: "point",
                                icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAkNJREFUOI2t1F1IU2Ecx/Hv2ebmbDpCXRLpfImIzAwrsGLrVYgkMMIrw4vqwigRgiCiq7ypLizMLrzQoDfCFIIuokAhwkwjWeGFb2x6UZq2ps52nO6cpwvz6Nocav6unsP/+X84/3N4HgPrHMPCovNT/2Ov77dTCCGtFtHrdIotNam+YHf2LQ0ccI+Xna3pXjW2kLY7hyuARVBR1DBsj81Ij3eOoCIAcGw2MeibY0RWATiQZuLDaFDbr6roYMnIS1O81UJLYxmuL0MUVr4B4GntaR497+RGs4eLjlTqbpfy6nU3JdUfw3qjgkO/grg9Pxj0jEcdb+h7AM/wGAMeb0QtAjTpJV7cLSYnO41Mu437kwEqG3q1eu257VwodxJn0FNVcZy9+ekcudq2PHjtVAabbFaMcQamZmSKi/LCwP37slBCCuZ4I/LMLHm56bHf0FGYhSzPwkZItMTj98sU2c1aXUJickrGYjETCAQJhZTYYKY9BZMpbr5ZkgBB6cmciG+1XCLAB43t1FSf0Z6n/DL1Lf2cOLYTgPYuN0cd27BaEwB429YTG0zcYKRv4BsZW1IQwE+vn89js1q96mEvHQV2fL5pEhJMlN9zxQZvvhxGUVQunXeiqgLnldYVjxsV/N+sCRRCoKoqQoiVgc3vR5gOtKLXLx7vJ01ddLjmT05dYxeSDmzJ5oheDZSWXA19EyH6WkfDNl5vcmvrZ66Jv6sJ/o0G2jOSvzZc3pW7pvtQ0glrkvldGHjo4I781ULRsu5/+Q+MfsaRRpWIsAAAAABJRU5ErkJggg=="
                            },
                        ],
                    },
                    {
                        id: "12",
                        text: "Lễ hội",
                        type: "point",
                        icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAATCAYAAAByUDbMAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAALdJREFUOI1jYaAiYGFgYGBgL/pv+/M/gzC5hkgyMNx93s94mYWBgYHh51+GdgYGBmtyDXvH8H8iAwNDAQuyoGdZJwOH0FGSDDoxYRnDuxfcDAwMUG/CAAvHOwYWjs2kOYvpP0I/LjUpxxkYOL5jl7svz8CwVRlTHKdh1lMZGLiXYpfjX0WiYeSAUcMGq2GJSxgYGJZQyTByAO0M29zUycDA0EmyIewM/zENoxRADJvIaEOJIT+hNADgTCOB9+T7kgAAAABJRU5ErkJggg=="
                    },
                    {
                        id: "13",
                        text: "Phản ánh hiện trường",
                        type: "point",
                        icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAALCAYAAACprHcmAAAAAXNSR0IB2cksfwAAAAlwSFlzAAAOxAAADsQBlSsOGwAAAM9JREFUGJV9zrEuREEYBeBv7k4ykSi9wtZK8Rb0VqUhokOpEYlE6BQaCq1XUGuFQqPxAAqy4pcwinvFWtZJpjnzTeZkqExrz6S8JJ5ypcEp+qi1lMsUcVOZS8x3+KEyyFrcxyw2m4iDSm44++A4sYqMqTzy1TMOK9vYrSxiX4vpXnwlsKGUZRFVKY8ieqPDR/EMjkTAQMQVLibhNqXcpYjzyg4W/sf0KmtYGb/4jSNecaKU9W7SD/xeuU3f3Ru2RAxx3XX3GOZExdIfc/bGi0+S6kGufyiU7gAAAABJRU5ErkJggg=="
                    },
                ];

                let tree = new Tree('#map-sidebar', {
                    data: data,
                    closeDepth: 3,
                    loaded: function () {
                        this.values = ['561', '562', '563', '564', '565', '00', '01', '02', '03', '05', '06', '07', '08', '11', '12'];
                    },
                    onChange: function () {
                        map.layers.items.forEach((el, idx) => {
                            const layerId = el.id;
                            if (this.values.includes(layerId) || el.type === "graphics") {
                                el.visible = true;
                            }
                            else {
                                if (el.sourceJSON && el.sourceJSON.types) {
                                    const type = el.sourceJSON.types;
                                    const field = el.sourceJSON.typeIdField;
                                    const intersection = type.filter(element => this.values.includes(element.name));
                                    if (intersection.length > 0) {
                                        let arr = [];
                                        intersection.forEach((el) => {
                                            arr.push(el.id);
                                        })
                                        if (field != null && field != "") {
                                            const df = `${field} IN (${returnQuery(arr)})`;
                                            el.definitionExpression = df;
                                            el.visible = true;
                                        }
                                        else {
                                            el.visible = false;
                                        }
                                    }
                                    else {
                                        el.visible = false;
                                    }
                                }
                                else {
                                    el.visible = false;
                                }
                            }
                        })
                    },
                    onItemClick: function () {

                    }
                });

                function returnQuery(slt) {
                    let str = ''
                    for (let i = 0; i < slt.length; i++) {
                        if (i == 0) {
                            str += `'${slt[i]}'`;
                        } else {
                            str += `,'${slt[i]}'`;
                        }
                    }
                    return str;
                }

                function setTreeCheck(id) {
                    let c = tree.values;
                    if (!(c.includes(id))) {
                        c.push(id);
                        tree.values = c;
                    }
                };

                function checkEmptyBlank(str) {
                    if (str.trim().length === 0 || str == null) {
                        return true
                    }
                    return false
                }
            }

        }, 2000)
    });
});

document.getElementById('map-sidebar-header').addEventListener('click', evt => {
    document.querySelector('#map-slider').classList.toggle('toggle-display');
})

document.getElementById('filter-sidebar-header').addEventListener('click', evt => {
    document.querySelector('#filter-slider').classList.toggle('toggle-display');
})

document.getElementById('result-sidebar-header').addEventListener('click', evt => {
    document.querySelector('#filter-slider').classList.toggle('toggle-display');
    document.querySelector('#result-slider').classList.toggle('toggle-display');
})