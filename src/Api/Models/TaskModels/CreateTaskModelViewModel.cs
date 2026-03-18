using System.ComponentModel.DataAnnotations;

namespace Api.Models.TaskModels;

public sealed class CreateTaskModelViewModel
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(128, ErrorMessage = "Title cannot exceed 128 characters.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(512, ErrorMessage = "Description cannot exceed 512 characters.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Priority is required.")]
    public string Priority { get; set; } = "Medium";

    [Required(ErrorMessage = "Deadline is required.")]
    [DataType(DataType.DateTime)]
    public DateTime Deadline { get; set; } = DateTime.Now.ToUniversalTime();
}
