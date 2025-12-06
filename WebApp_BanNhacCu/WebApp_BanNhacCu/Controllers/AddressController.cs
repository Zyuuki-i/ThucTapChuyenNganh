using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Controllers
{
    public class AddressController : Controller
    {
        public async Task<IActionResult> GetFullProvinceData()
        {
            HttpClient client = new HttpClient();

            var jsonProvince = await client.GetStringAsync("https://provinces.open-api.vn/api/v2/p/");
            List<Province>? provinces = JsonConvert.DeserializeObject<List<Province>>(jsonProvince);

            foreach (Province p in provinces)
            {
                string url = $"https://provinces.open-api.vn/api/v2/p/{p.code}/?depth=2";

                var jsonDetail = await client.GetStringAsync(url);

                Province? provinceDetail = JsonConvert.DeserializeObject<Province>(jsonDetail);

                p.wards = provinceDetail.wards;
            }

            return Json(provinces);
        }

    }
}
