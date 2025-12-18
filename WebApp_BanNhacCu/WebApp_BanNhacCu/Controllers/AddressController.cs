using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Controllers
{
    public class AddressController : Controller
    {
        public async Task<IActionResult> GetFullProvinceData()
        {
            var provinces = new List<Province>
            {
                new Province {
                    code = 1,
                    name = "Thành phố Hà Nội",
                    division_type = "thành phố trung ương",
                    codename = "ha_noi",
                    phone_code = 24,
                    wards = new List<Ward> {
                        new Ward { code = 1, name = "Phường Phúc Xá", division_type = "phường", codename = "phuc_xa", province_code = 1 },
                        new Ward { code = 2, name = "Phường Trúc Bạch", division_type = "phường", codename = "truc_bach", province_code = 1 },
                        new Ward { code = 3, name = "Phường Vĩnh Phúc", division_type = "phường", codename = "vinh_phuc", province_code = 1 },
                        new Ward { code = 4, name = "Phường Cống Vị", division_type = "phường", codename = "cong_vi", province_code = 1 },
                        new Ward { code = 5, name = "Phường Liễu Giai", division_type = "phường", codename = "lieu_giai", province_code = 1 },
                        new Ward { code = 6, name = "Phường Nguyễn Trung Trực", division_type = "phường", codename = "nguyen_trung_truc", province_code = 1 },

                        new Ward { code = 7, name = "Phường Hàng Bạc", division_type = "phường", codename = "hang_bac", province_code = 1 },
                        new Ward { code = 8, name = "Phường Hàng Đào", division_type = "phường", codename = "hang_dao", province_code = 1 },
                        new Ward { code = 9, name = "Phường Hàng Gai", division_type = "phường", codename = "hang_gai", province_code = 1 },
                        new Ward { code = 10, name = "Phường Hàng Mã", division_type = "phường", codename = "hang_ma", province_code = 1 },

                        new Ward { code = 11, name = "Phường Láng Hạ", division_type = "phường", codename = "lang_ha", province_code = 1 },
                        new Ward { code = 12, name = "Phường Ô Chợ Dừa", division_type = "phường", codename = "o_cho_dua", province_code = 1 },
                        new Ward { code = 13, name = "Phường Kim Liên", division_type = "phường", codename = "kim_lien", province_code = 1 },
                        new Ward { code = 14, name = "Phường Phương Mai", division_type = "phường", codename = "phuong_mai", province_code = 1 },

                        new Ward { code = 15, name = "Phường Dịch Vọng", division_type = "phường", codename = "dich_vong", province_code = 1 },
                        new Ward { code = 16, name = "Phường Nghĩa Tân", division_type = "phường", codename = "nghia_tan", province_code = 1 },
                        new Ward { code = 17, name = "Phường Mai Dịch", division_type = "phường", codename = "mai_dich", province_code = 1 }
                    }
                },
               new Province {
                    code = 79,
                    name = "Thành phố Hồ Chí Minh",
                    division_type = "thành phố trung ương",
                    codename = "ho_chi_minh",
                    phone_code = 28,
                    wards = new List<Ward> {
                        new Ward { code = 26734, name = "Phường Bến Nghé", division_type = "phường", codename = "ben_nghe", province_code = 79 },
                        new Ward { code = 26737, name = "Phường Bến Thành", division_type = "phường", codename = "ben_thanh", province_code = 79 },
                        new Ward { code = 26740, name = "Phường Nguyễn Thái Bình", division_type = "phường", codename = "nguyen_thai_binh", province_code = 79 },
                        new Ward { code = 26743, name = "Phường Phạm Ngũ Lão", division_type = "phường", codename = "pham_ngu_lao", province_code = 79 },
                        new Ward { code = 26746, name = "Phường Cầu Ông Lãnh", division_type = "phường", codename = "cau_ong_lanh", province_code = 79 },
                        new Ward { code = 26745, name = "Phường Xuân Hòa", division_type = "phường", codename = "xuan_hoa", province_code = 79 },

                        new Ward { code = 26749, name = "Phường Võ Thị Sáu", division_type = "phường", codename = "vo_thi_sau", province_code = 79 },

                        new Ward { code = 26758, name = "Phường 1", division_type = "phường", codename = "phuong_1", province_code = 79 },
                        new Ward { code = 26770, name = "Phường 2", division_type = "phường", codename = "phuong_2", province_code = 79 },
                        new Ward { code = 26761, name = "Phường 4", division_type = "phường", codename = "phuong_4", province_code = 79 },
                        new Ward { code = 26764, name = "Phường 5", division_type = "phường", codename = "phuong_5", province_code = 79 },

                        new Ward { code = 26752, name = "Phường 7", division_type = "phường", codename = "phuong_7", province_code = 79 },
                        new Ward { code = 26773, name = "Phường 13", division_type = "phường", codename = "phuong_13", province_code = 79 },
                        new Ward { code = 26755, name = "Phường 14", division_type = "phường", codename = "phuong_14", province_code = 79 },

                        new Ward { code = 26776, name = "Phường Linh Trung", division_type = "phường", codename = "linh_trung", province_code = 79 },
                        new Ward { code = 26779, name = "Phường Linh Xuân", division_type = "phường", codename = "linh_xuan", province_code = 79 },
                        new Ward { code = 26782, name = "Phường Hiệp Bình Chánh", division_type = "phường", codename = "hiep_binh_chanh", province_code = 79 },
                        new Ward { code = 26783, name = "Phường Thạnh Mỹ Tây", division_type = "phường", codename = "thanh_my_tay", province_code = 79 },
                        new Ward { code = 26784, name = "Phường Phú Nhuận", division_type = "phường", codename = "phu_nhuan", province_code = 79 }
                    }
                },
                new Province {
                    code = 68, name = "Tỉnh Lâm Đồng", division_type = "tỉnh", codename = "lam_dong", phone_code = 263,
                    wards = new List<Ward> {
                        new Ward { code = 24820, name = "Phường La Gi", division_type = "phường", codename = "la_gi", province_code = 68 },
                        new Ward { code = 24823, name = "Phường Bắc Gia Nghĩa", division_type = "phường", codename = "bac_gia_nghia", province_code = 68 }
                    }
                },
                new Province {
                    code = 93, name = "Tỉnh Hậu Giang", division_type = "tỉnh", codename = "hau_giang", phone_code = 293,
                    wards = new List<Ward> {
                        new Ward { code = 31333, name = "Phường Ngã Bảy", division_type = "phường", codename = "nga_bay", province_code = 93 },
                        new Ward { code = 31336, name = "Phường Hiệp Thành", division_type = "phường", codename = "hiep_thanh", province_code = 93 }
                    }
                },
                new Province {
                    code = 96, name = "Tỉnh Cà Mau", division_type = "tỉnh", codename = "ca_mau", phone_code = 290,
                    wards = new List<Ward> {
                        new Ward { code = 32248, name = "Phường Hoà Thành", division_type = "phường", codename = "hoa_thanh", province_code = 96 },
                        new Ward { code = 32251, name = "Phường Tân Thành", division_type = "phường", codename = "tan_thanh", province_code = 96 }
                    }
                }
            };
            return Json(provinces);
        }

        //public async Task<IActionResult> GetFullProvinceData()
        //{
        //    HttpClient client = new HttpClient();

        //    var jsonProvince = await client.GetStringAsync("https://provinces.open-api.vn/api/v2/p/");
        //    List<Province>? provinces = JsonConvert.DeserializeObject<List<Province>>(jsonProvince);

        //    foreach (Province p in provinces)
        //    {
        //        string url = $"https://provinces.open-api.vn/api/v2/p/{p.code}/?depth=2";

        //        var jsonDetail = await client.GetStringAsync(url);

        //        Province? provinceDetail = JsonConvert.DeserializeObject<Province>(jsonDetail);

        //        p.wards = provinceDetail.wards;
        //    }

        //    return Json(provinces);
        //}

    }
}
