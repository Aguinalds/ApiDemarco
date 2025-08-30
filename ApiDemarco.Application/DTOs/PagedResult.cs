namespace ApiDemarco.Application.DTOs;

public class PagedResult<T>
{
    public required IEnumerable<T> items { get; set; }
    public int totalItems { get; set; }
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
    public int totalPages => (int)Math.Ceiling((double)totalItems / pageSize);
    
}