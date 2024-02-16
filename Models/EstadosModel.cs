using System;

namespace Empresa_api.Models;

public class EstadosModel
{
    public Guid ID_Estado { get; set; }
    public string? nombre {  get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool Active { get; set; }
}
