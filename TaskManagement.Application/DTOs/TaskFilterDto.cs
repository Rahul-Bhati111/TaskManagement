
namespace TaskManagement.Application.DTOs;

public class TaskFilterDto
{
	public int? AssignedUserId { get; set; }

	public bool UnassignedOnly { get; set; }
}
