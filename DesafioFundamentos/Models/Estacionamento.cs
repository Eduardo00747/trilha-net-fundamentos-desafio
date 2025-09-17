using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<Veiculo> veiculos = new List<Veiculo>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        private string NormalizarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return "";

            placa = placa.ToUpper().Replace(" ", "");

            // Normalização: se digitar ABC1234 → vira ABC-1234
            if (Regex.IsMatch(placa, @"^[A-Z]{3}\d{4}$"))
                placa = placa.Insert(3, "-");

            return placa;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar (Ex: RDE-1234 ou RDE1A23):");
            string placa = NormalizarPlaca(Console.ReadLine());

            string padraoAntigo = @"^[A-Z]{3}-\d{4}$";
            string padraoMercosul = @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$";

            if (!string.IsNullOrWhiteSpace(placa) &&
                (Regex.IsMatch(placa, padraoAntigo) || Regex.IsMatch(placa, padraoMercosul)))
            {
                veiculos.Add(new Veiculo(placa));
                Console.WriteLine($"Veículo {placa} adicionado com sucesso às {DateTime.Now:t}!");
            }
            else
            {
                Console.WriteLine("Placa inválida. Use o formato correto (Ex: RDE-1234 ou RDE1A23).");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = NormalizarPlaca(Console.ReadLine());

            var veiculo = veiculos.FirstOrDefault(v => v.Placa == placa);

            if (veiculo != null)
            {
                DateTime horaSaida = DateTime.Now;
                TimeSpan tempo = horaSaida - veiculo.HoraEntrada;

                // Arredondar horas para cima (ex: 1h20m → 2h)
                int horas = (int)Math.Ceiling(tempo.TotalHours);

                decimal valorTotal = precoInicial + precoPorHora * horas;

                veiculos.Remove(veiculo);

                Console.WriteLine($"O veículo {placa} foi removido.");
                Console.WriteLine($"Tempo estacionado: {horas} hora(s).");
                Console.WriteLine($"Preço total: R$ {valorTotal:F2}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui.");
            }
        }

        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (var v in veiculos)
                {
                    Console.WriteLine($"- {v.Placa} (entrada às {v.HoraEntrada:t})");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
