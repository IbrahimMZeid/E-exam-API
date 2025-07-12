namespace E_exam.DTOs.UserDTOs
{
    public class UserStudentDTO
    {
        // student
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        //user
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; }
    }
}
