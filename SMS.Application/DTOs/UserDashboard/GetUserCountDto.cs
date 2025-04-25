namespace SMS.Application.DTOs.UserDashboard
{
    public class GetUserCountDto
    {
        public int TotalTeachersAdded { get; set; }

        public decimal TeachersGrowthPercent { get; set; }

        public int TotalStudentsAdded { get; set; }

        public decimal StudentsGrowthPercent { get; set; }

        public int RecentActivityCount { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
