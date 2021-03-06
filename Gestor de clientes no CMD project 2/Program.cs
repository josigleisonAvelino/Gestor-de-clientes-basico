using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace Gestor_de_clientes_no_CMD_project_2
{
    class Program
    {
        [System.Serializable]
        struct Cliente 
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();


        enum Menu { Listagem = 1 , Adicionar = 2, Remover = 3, Sair = 4 }

        static void Main(string[] args)
        {
            bool EscolheuSair = false;
            while (!EscolheuSair)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("Sistema de clientes Bem-vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        EscolheuSair = true;
                        break;

                }
                Console.Clear();
            }
            


        }
        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente:");
            Console.WriteLine("Nome do cliente:");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente:");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente:");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluido tecle enter para sair!");
            Console.ReadLine();
        }
        static void Listagem()
        {
            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de clientes:");
                int i = 0;
                foreach(Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID:{i}");
                    Console.WriteLine($"nome:{cliente.nome}");
                    Console.WriteLine($"email:{cliente.email}");
                    Console.WriteLine($"CPF:{cliente.cpf}");
                    Console.WriteLine("-----------------------");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado!");
            }
            Console.WriteLine("Aperte enter para sair");
            Console.ReadLine();
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("DIgite o ID do cliente que deseja remover:");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("ID Digitado é inválido tente novamente!");
                Console.ReadLine();
            }
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);

            stream.Close();
        }
        static void Carregar()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);
                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }
            }
            catch(Exception e)
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        }
    }
}
