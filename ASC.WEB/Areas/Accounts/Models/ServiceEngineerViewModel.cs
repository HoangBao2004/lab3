using Microsoft.AspNetCore.Identity;

namespace ASC.WEB.Areas.Accounts.Models
{
    public class ServiceEngineerViewModel
    {
        public List<IdentityUser> ServiceEngineers { get; set; } // Lưu trữ danh sách nhân viên

        public ServiceEngineerRegistrationViewModel Registration { get; set; } // Lưu trữ nhân viên thêm mới hoặc cập nhật

        // Cờ kiểm tra xem có phải đang ở chế độ chỉnh sửa hay không
        public bool IsEditMode
        {
            get
            {
                return Registration != null && Registration.IsEdit;
            }
        }

        // Cờ kiểm tra trạng thái hoạt động của nhân viên đang đăng ký
        public bool IsActiveEngineer
        {
            get
            {
                return Registration != null && Registration.IsActive;
            }
        }
    }
}
