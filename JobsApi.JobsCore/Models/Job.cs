using System.ComponentModel.DataAnnotations;

namespace JobsApi.JobsCore.Models
{
    public class Job
    {
        public string JobId { get; set; }
        
        public string JobName { get; set; }
        
        public string JobDescription { get; set; }
        
        public string Username { get; set; }
        
        public long CreatedAt { get; set; }
        
        public bool Completed { get; set; }
    }
    
    public class JobCreateDto
    {
        [Required]
        public string JobName { get; set; }
        
        [Required]
        public string JobDescription { get; set; }
        
        [StringLength(20)]
        [Required]
        public string Username { get; set; }
    }

    public class JobGetDto
    {
        [StringLength(20)]
        [Required]
        public string Username { get; set; }
        
        public string JobId { get; set; }
    }

    public class JobListGetDto
    {
        [StringLength(20)]
        [Required]
        public string Username { get; set; }
    }
}