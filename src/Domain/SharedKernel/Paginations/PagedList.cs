namespace Domain.SharedKernel.Paginations;

/// <summary>
/// Represents a paginated list of items with information about pagination.
/// </summary>
/// <typeparam name="TItem">The type of the items contained in the paginated list.</typeparam>
public class PagedList<TItem>
{
    /// <summary>
    /// Gets the collection of items on the current page.
    /// </summary>
    public IEnumerable<TItem> Items { get; private set; }

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int CurrentPage { get; private set; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; private set; }

    /// <summary>
    /// Gets the page size (the number of items per page).
    /// </summary>
    public int PageSize { get; private set; }

    /// <summary>
    /// Gets the total count of items across all pages.
    /// </summary>
    public int TotalCount { get; private set; }

    /// <summary>
    /// Indicates whether there's a previous page available.
    /// </summary>
    public bool HasPrevious => CurrentPage > 1;

    /// <summary>
    /// Indicates whether there's a next page available.
    /// </summary>
    public bool HasNext => CurrentPage < TotalPages;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public PagedList() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="items">The items for the current page.</param>
    /// <param name="count">The total number of items.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    public PagedList(IEnumerable<TItem> items, int count, int pageNumber, int pageSize)
    {
        ArgumentNullException.ThrowIfNull(items);

        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(
                nameof(pageNumber), "Page number must be greater than 0.");
        if (count < 0)
            throw new ArgumentOutOfRangeException(
                nameof(count), "Total count cannot be negative.");

        Items = items;
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    public static PagedList<TItem> Empty(int pageNumber, int pageSize) =>
        new([], 0, pageNumber, pageSize);

    public static int GetSkip(int pageNumber, int pageSize) =>
        (pageNumber - 1) * pageSize;

    public static int GetTake(int pageSize) => pageSize;

}
