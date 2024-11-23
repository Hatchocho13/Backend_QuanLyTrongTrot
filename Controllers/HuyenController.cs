using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_qltt2.Data;
using backend_qltt2.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend_qltt2.Controllers
{
    [Route("api/Huyens")]
    [ApiController]
    public class HuyenController : ControllerBase
    {
        private readonly QuanLyTrongTrotContext _context;

        public HuyenController(QuanLyTrongTrotContext context)
        {
            _context = context;
        }

        // GET: api/Huyen
        [HttpGet]
        public ActionResult<IEnumerable<Huyen>> GetHuyens()
        {
            // Thực hiện truy vấn SQL thủ công
            var results = _context.Huyens.FromSqlRaw("SELECT * FROM Huyen").ToList();
            return Ok(results);
        }
    }
}
