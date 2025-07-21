namespace BusinessObjects.Models.BaseModels;

public interface IBaseModel
{
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }  
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}
