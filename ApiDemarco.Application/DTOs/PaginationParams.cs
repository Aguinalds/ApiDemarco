namespace ApiDemarco.Application.DTOs;

public class PaginationParams
{
    private const int maxPageSize = 100;
    private int _pageSize = 10;

    public int pageNumber { get; set; } = 1;
    public int pageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }

    public PaginationParams() { }

    public PaginationParams(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber;
        pageSize = (pageSize > maxPageSize) ? maxPageSize : pageSize;
    }
}