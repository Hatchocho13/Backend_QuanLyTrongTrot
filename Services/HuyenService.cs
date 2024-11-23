using backend_qltt2.Data;
using backend_qltt2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend_qltt2.Services
{
    public class HuyenService
    {
        private readonly QuanLyTrongTrotContext _context;

        // Constructor nhận QuanLyTrongTrotContext để thao tác với cơ sở dữ liệu
        public HuyenService(QuanLyTrongTrotContext context)
        {
            _context = context;
        }

        // Phương thức lấy danh sách tên huyện
        public List<string> GetAllHuyenNames()
        {
            // Truy vấn lấy danh sách tên huyện từ bảng Huyens
            var huyenNames = _context.Huyens
                                     .Select(h => h.TenHuyen) // Lấy chỉ trường TenHuyen (giả sử trường này tồn tại)
                                     .ToList(); // Thực thi truy vấn và lấy kết quả dưới dạng danh sách

            return huyenNames;
        }
    }
}
