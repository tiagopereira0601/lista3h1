
using System.Data.SqlClient;

public class CursoRepository : ICursoRepository
{
    private readonly string _connectionString;

    public CursoRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public void Criar(Curso curso)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        var cmd = new SqlCommand(
            "INSERT INTO Curso (IdCurso, Nome, NomeCoordenador, Ativo) VALUES (@id, @nome, @coord, @ativo)", con);
        cmd.Parameters.AddWithValue("@id", curso.IdCurso);
        cmd.Parameters.AddWithValue("@nome", curso.Nome);
        cmd.Parameters.AddWithValue("@coord", curso.NomeCoordenador);
        cmd.Parameters.AddWithValue("@ativo", curso.Ativo);
        cmd.ExecuteNonQuery();
    }

    public void Atualizar(Curso curso)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        var cmd = new SqlCommand(
            "UPDATE Curso SET Nome=@nome, NomeCoordenador=@coord, Ativo=@ativo WHERE IdCurso=@id", con);
        cmd.Parameters.AddWithValue("@id", curso.IdCurso);
        cmd.Parameters.AddWithValue("@nome", curso.Nome);
        cmd.Parameters.AddWithValue("@coord", curso.NomeCoordenador);
        cmd.Parameters.AddWithValue("@ativo", curso.Ativo);
        cmd.ExecuteNonQuery();
    }

    public void Excluir(int idCurso)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        var cmd = new SqlCommand("DELETE FROM Curso WHERE IdCurso=@id", con);
        cmd.Parameters.AddWithValue("@id", idCurso);
        cmd.ExecuteNonQuery();
    }

    public Curso Buscar(int idCurso)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        var cmd = new SqlCommand("SELECT * FROM Curso WHERE IdCurso=@id", con);
        cmd.Parameters.AddWithValue("@id", idCurso);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Curso
            {
                IdCurso = (int)reader["IdCurso"],
                Nome = reader["Nome"].ToString(),
                NomeCoordenador = reader["NomeCoordenador"].ToString(),
                Ativo = (bool)reader["Ativo"]
            };
        }
        return null;
    }

    public List<Curso> BuscarTodos()
    {
        var lista = new List<Curso>();
        using var con = new SqlConnection(_connectionString);
        con.Open();
        var cmd = new SqlCommand("SELECT * FROM Curso", con);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Curso
            {
                IdCurso = (int)reader["IdCurso"],
                Nome = reader["Nome"].ToString(),
                NomeCoordenador = reader["NomeCoordenador"].ToString(),
                Ativo = (bool)reader["Ativo"]
            });
        }
        return lista;
    }
}
