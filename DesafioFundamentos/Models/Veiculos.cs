namespace DesafioFundamentos.Models
{
    public class Veiculo
    {
        public string Placa { get; set; }
        public DateTime HoraEntrada { get; set; }

        public Veiculo(string placa)
        {
            Placa = placa;
            // registra a hora de entrada automaticamente
            HoraEntrada = DateTime.Now;
        }
    }
}
