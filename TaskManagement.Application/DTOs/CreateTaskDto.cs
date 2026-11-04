using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs;

public class CreateTaskDto
{
	[Required(ErrorMessage = "Task title is required.")]
	[StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
	public string Title { get; set; } = string.Empty;

	public string? Description { get; set; }

	[Required(ErrorMessage = "Due date is required.")]
	public DateTime DueDate { get; set; }

	[Range(1, short.MaxValue, ErrorMessage = "Please select a priority.")]
	public short PriorityId { get; set; }

	public int? AssignedUserId { get; set; }
}