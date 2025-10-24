namespace OrderManager.Models
{
    /// <summary>
    /// The work order status enumeration.
    /// A work order starts as OPEN, can be set to CLOSED when completed, or CANCELLED if aborted.
    /// </summary>
    public enum WorkOrderStatusEnum
    {
        OPEN, CLOSED, CANCELLED
    }
}
