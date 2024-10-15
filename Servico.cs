using System.Data;

public class Servico {
    public String Id { get; set; }
    public int VeiculoId { get; set; }
    public DateTime Data { get; set; }
    public string TipoServico { get; set; }
    public decimal Valor { get; set; }
    public string Status { get; set; }
}