const MAP_SR = 'PROJCS["dh10000",GEOGCS["dh10000",DATUM["D_WGS_1984",SPHEROID["WGS_1984",6378137.0,298.257223563]],PRIMEM["Greenwich",0.0],UNIT["Degree",0.0174532925199433]],PROJECTION["Transverse_Mercator"],PARAMETER["False_Easting",500000.0],PARAMETER["False_Northing",0.0],PARAMETER["Central_Meridian",108.0],PARAMETER["Scale_Factor",0.9999],PARAMETER["Latitude_Of_Origin",0.0],UNIT["Meter",1.0]],VERTCS["VN2000",DATUM["D_WGS_1984",SPHEROID["WGS_1984",6378137.0,298.257223563]],PARAMETER["Vertical_Shift",0.0],PARAMETER["Direction",1.0],UNIT["Meter",1.0]]';

let polylineGraphic = null;
let graphicsLayer = null;

$(document).ready(function () {
    $(".select2").select2({
        width: '100%',
    });

    $(".select2-hidesearch").select2({
        width: '100%',
        minimumResultsForSearch: -1
    });

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
        "esri/widgets/Search",
        "esri/widgets/Locate",
        "esri/Graphic",
        "esri/layers/GraphicsLayer",
        "esri/geometry/Point",
        "esri/geometry/Polyline",
        "esri/geometry/SpatialReference",
        "esri/geometry/projection",
        "esri/rest/support/Query"
    ], function (esriConfig, intl, Map, MapView, FeatureLayer, MapImageLayer, TileLayer, Basemap, BasemapGallery, Expand, Search, Locate, Graphic, GraphicsLayer, Point, Polyline, SpatialReference, projection, Query) {
        const mApiKey = 'AAPKf17a300c1b284fca90afee0ecdd6fbdfqxum1STGOMVETG7zr4IxM10ieTxpbtbZGXvDaq01a9c54BH-vHhQY7Zg3WknPs40';
        esriConfig.apiKey = mApiKey;

        intl.setLocale("vi");

        const imageLayer = new MapImageLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BDHC/HueBDHC/MapServer",
            id: "05"
        });

        const basemapData = [
            {
                id: "arcgis-streets",
                text: "Đường phố",
            },
            {
                id: "arcgis-topographic",
                text: "Địa hình",
            },
            {
                id: "satellite",
                text: "Vệ tinh",
            },
        ]

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
            }
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
            if (found && found.child && found.child.length > 0) {
                found.child.forEach((el) => {
                    $('#filter-lh-slt').append(
                        `<option value="${el.lid}">${el.text}</option>`
                    );
                })
            }
        })

        $("#filter-lv-slt").val($("#filter-lv-slt option:first").val()).trigger('change');

        //const mLayer = new WMSLayer({
        //    url: 'https://gishue.thuathienhue.gov.vn/server/services/BDHC/HueBDHC/MapServer/WMSServer'
        //})

        //FeatureLayer Group

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

        //DiaPhuong
        const table_15 = new FeatureLayer({
            url: "https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/15",
            id: "15",
            outFields: ["*"],
        });

        //FeatureLayer Group

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

        view.when(() => {
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
                }
            });
            view.ui.add(btnClearRoute, "top-right");

            //Locate Btn
            const locateBtn = new Locate({
                view: view
            });

            view.ui.add(locateBtn, {
                position: "top-left"
            });

            view.popup.on("trigger-action", (evt) => {
                if (evt.action.id === "routes") {
                    const mPopup = view.popup;
                    let mlat = null;
                    let mLong = null;

                    if (mPopup.selectedFeature.geometry.latitude && mPopup.selectedFeature.geometry.longitude) {
                        mLat = mPopup.selectedFeature.geometry.latitude;
                        mLong = mPopup.selectedFeature.geometry.longitude;

                        if (polylineGraphic) {
                            graphicsLayer.remove(polylineGraphic);
                        }

                        locateBtn.locate().then(function (pos) {
                            const { latitude: curLat, longitude: curLong } = pos.coords
                            fetch(`https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf624851a99071ab2944ee85a0f5e9f5e40c36&start=${curLong},${curLat}&end=${mLong},${mLat}`)
                                .then(res => {
                                    //console.log(res)
                                    return res.json()
                                })
                                .then(data => {
                                    //console.log(data.features[0].geometry.coordinates)
                                    const { features } = data;
                                    const [resultFeature] = features;
                                    const { geometry, properties } = resultFeature;
                                    const { segments } = properties;
                                    const [segmentResult] = segments
                                    const { steps } = segmentResult
                                    const { coordinates } = geometry;
                                    console.log(steps);

                                    steps.forEach((el) => {
                                        $.i18n._('some text')
                                    })

                                    polylineGraphic = new Graphic({
                                        geometry: new Polyline({
                                            hasZ: false,
                                            hasM: true,
                                            paths: [...coordinates],
                                            //spatialReference: SpatialReference.WGS84

                                            //spatialReference: new SpatialReference({
                                            //    wkt: 'GEOGCS["WGS 84",DATUM["WGS_1984",SPHEROID["WGS 84",6378137,298.257223563,AUTHORITY["EPSG","7030"]],AUTHORITY["EPSG","6326"]],PRIMEM["Greenwich",0,AUTHORITY["EPSG","8901"]],UNIT["degree",0.0174532925199433,AUTHORITY["EPSG","9122"]],AUTHORITY["EPSG","4326"]]',
                                            //})
                                        }),
                                        symbol: {
                                            type: "simple-line",
                                            color: [226, 119, 40], // Orange
                                            width: 3
                                        }
                                    });

                                    //console.log(graphicsLayer.graphics);
                                    graphicsLayer.add(polylineGraphic);
                                })
                                .catch(er => {
                                    console.log(er)
                                })
                        });
                    } else {
                        const point = new Point(
                            mPopup.selectedFeature.geometry.x,
                            mPopup.selectedFeature.geometry.y,
                            new SpatialReference({
                                wkt: 'PROJCS["dh10000",GEOGCS["dh10000",DATUM["D_WGS_1984",SPHEROID["WGS_1984",6378137.0,298.257223563]],PRIMEM["Greenwich",0.0],UNIT["Degree",0.0174532925199433]],PROJECTION["Transverse_Mercator"],PARAMETER["False_Easting",500000.0],PARAMETER["False_Northing",0.0],PARAMETER["Central_Meridian",108.0],PARAMETER["Scale_Factor",0.9999],PARAMETER["Latitude_Of_Origin",0.0],UNIT["Meter",1.0]],VERTCS["VN2000",DATUM["D_WGS_1984",SPHEROID["WGS_1984",6378137.0,298.257223563]],PARAMETER["Vertical_Shift",0.0],PARAMETER["Direction",1.0],UNIT["Meter",1.0]]',
                            })
                        );

                        let outSpatialReference = new SpatialReference({ wkid: 4326 });

                        projection.load().then(function () {
                            const res = projection.project(point, outSpatialReference);

                            if (res != null && res.longitude != null && res.latitude != null) {
                                mLat = res.latitude;
                                mLong = res.longitude;

                                if (polylineGraphic) {
                                    graphicsLayer.remove(polylineGraphic);
                                }

                                locateBtn.locate().then(function (pos) {
                                    const { latitude: curLat, longitude: curLong } = pos.coords
                                    fetch(`https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf624851a99071ab2944ee85a0f5e9f5e40c36&start=${curLong},${curLat}&end=${mLong},${mLat}`)
                                        .then(res => {
                                            //console.log(res)
                                            return res.json()
                                        })
                                        .then(data => {
                                            //console.log(data.features[0].geometry.coordinates)
                                            const { features } = data;
                                            const [resultFeature] = features;
                                            const { geometry, properties } = resultFeature;
                                            const { segments } = properties;
                                            const [segmentResult] = segments
                                            const { steps } = segmentResult
                                            const { coordinates } = geometry;
                                            //console.log(steps);

                                            polylineGraphic = new Graphic({
                                                geometry: new Polyline({
                                                    hasZ: false,
                                                    hasM: true,
                                                    paths: [...coordinates],
                                                    //spatialReference: SpatialReference.WGS84

                                                    //spatialReference: new SpatialReference({
                                                    //    wkt: 'GEOGCS["WGS 84",DATUM["WGS_1984",SPHEROID["WGS 84",6378137,298.257223563,AUTHORITY["EPSG","7030"]],AUTHORITY["EPSG","6326"]],PRIMEM["Greenwich",0,AUTHORITY["EPSG","8901"]],UNIT["degree",0.0174532925199433,AUTHORITY["EPSG","9122"]],AUTHORITY["EPSG","4326"]]',
                                                    //})
                                                }),
                                                symbol: {
                                                    type: "simple-line",
                                                    color: [226, 119, 40], // Orange
                                                    width: 3
                                                }
                                            });

                                            //console.log(graphicsLayer.graphics);
                                            graphicsLayer.add(polylineGraphic);
                                        })
                                        .catch(er => {
                                            console.log(er)
                                        })
                                });
                            } else {
                                alert('Nhập sai tọa độ!');
                            }
                        });
                    }
                }

                //const { latitude: mLat, longitude: mLong } = mPopup.selectedFeature.geometry



                //const selectedFeature =
                //    mPopup.selectedFeature && mPopup.selectedFeature.isAggregate;

                //// console.log(view.popup.selectedFeature.getObjectId());
                //displayFeature(mPopup.selectedFeature);

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
                    const lid = $('#filter-lh-slt').val();
                    if (lid) {
                        const layer = map.layers.items.find(x => (x.id == lid && x.type === 'feature'));
                        if (layer) {
                            $('#filter-btn').addClass('disabled')
                            resultArr = [];
                            if (type == "xq") {
                                //Tim kiem xung quanh
                                locateBtn.locate()
                                    .then(function (pos) {
                                        if (pos && pos.coords && pos.coords.latitude && pos.coords.longitude) {
                                            const point = new Point(
                                                pos.coords.longitude,
                                                pos.coords.latitude
                                            );

                                            const radius = $('#bk-input').val();

                                            $('#filter-slider').toggleClass('toggle-display');
                                            $('#result-slider').toggleClass('toggle-display');

                                            $('#filter-btn').removeClass('disabled')

                                            layer.queryFeatures({
                                                geometry: point,
                                                distance: radius,
                                                units: "meters",
                                                spatialRelationship: "intersects",
                                                returnGeometry: true,
                                                returnQueryGeometry: true,
                                                outFields: ["*"],
                                            })
                                                .then((res) => {
                                                    resultArr = res.features;
                                                    renderResult(resultArr)
                                                });
                                        }
                                    });
                            }
                            else if (type == "nc") {
                                //Tim kiem nang cao
                                const keyword = $('#search-input').val();

                                const clause = `madoanhngh LIKE '%${keyword}%' OR tendoituon LIKE '%${keyword}%' OR diachi LIKE '%${keyword}%' OR tennguoida LIKE '%${keyword}%' OR nganhnghe LIKE '%${keyword}%'`;

                                let arr = [];
                                arr.push(...(await getDataWithQuery(layer, clause)));

                                $('#filter-slider').toggleClass('toggle-display');
                                $('#result-slider').toggleClass('toggle-display');

                                $('#filter-btn').removeClass('disabled')

                                if (arr.length > 0) {
                                    resultArr = arr;
                                    renderResult(resultArr)
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
            });

            $('#filter-cancel-btn').on('click', function () {

            });

            $(document).on('click', '.result-list-item', function () {
                const id = $(this).data('oid');
                const found = resultArr.find(x => x.attributes.objectid == id);
                if (found) {
                    console.log(found)
                    view.goTo({
                        center: found.geometry,
                        zoom: 20
                    });
                    view.popup.open({
                        features: [found],
                        location: found.geometry
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

        //Query truong hop tren 1000
        async function getDataWithQuery(layer, clause) {
            let arr = [];

            const queryId = new Query();
            queryId.where = clause;

            const results = await layer.queryObjectIds(queryId);

            if (results != null) {
                const page = Math.ceil(results.length / 1000);

                for (let i = 0; i < page; i++) {
                    let start = i * 1000;
                    let end = (i + 1) * 1000;
                    var items = results.slice(start, end);

                    arr.push(...(await getFeatureById(layer, items)));
                }
            }

            return Promise.all(arr);
        };

        function renderResult(data) {
            $('#result-content').empty();
            let html = '';
            data.forEach((el) => {
                html += `
                            <div class="col-12 result-list-item" data-oid="${el.attributes.objectid}">
						        <h4>${el.attributes.tendoituon}</h4>
						        <p>${el.attributes.diachi}</p>
						        <p>${el.attributes.trangthai}</p>
					        </div>
                        `
            });

            $('#result-content').append(html);
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
            //{
            //    id: "05",
            //    text: "Bản đồ hành chính",
            //},
            //{
            //    id: "100",
            //    text: "Quận huyện",
            //},
            //{
            //    id: "101",
            //    text: "Phường xã",
            //},
        ];

        let tree = new Tree('#map-sidebar', {
            data: data,
            closeDepth: 3,
            loaded: function () {
                this.values = ['561', '562', '563', '564', '565', '00', '01', '02', '03', '05', '06', '07', '08', '11'];
            },
            onChange: function () {
                //console.log(map.layers.items);
                map.layers.items.forEach((el, idx) => {
                    if (el.type === "feature") {
                        const layerId = el.id;
                        if (this.values.includes(layerId) || el.type === "graphics") {
                            el.visible = true;
                        } else {
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
                                        console.log(el)
                                    }
                                }
                            }
                            else {
                                el.visible = false;
                            }
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
    });
});

document.getElementById('map-sidebar-header').addEventListener('click', evt => {
    document.querySelector('#map-slider').classList.toggle('toggle-display');
})

document.getElementById('filter-sidebar-header').addEventListener('click', evt => {
    document.querySelector('#filter-slider').classList.toggle('toggle-display');
})

document.getElementById('result-sidebar-header').addEventListener('click', evt => {
    document.querySelector('#result-slider').classList.toggle('toggle-display');
})