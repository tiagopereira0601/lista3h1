
using System.Data.SqlClient;

public class MatriculaRepository : IMatriculaRepository
{
    private readonly string _connectionString;

    public MatriculaRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public void Criar(Matricula matricula)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        var cmd = new SqlCommand(
            "INSERT INTO Matricula (IdAluno, IdCurso, DataMatricula, Ativa) VALUES (@aluno, @curso, @data, @ativa)",
            con);
        cmd.Parameters.AddWithValue("@aluno", matricula.IdAluno);
        cmd.Parameters.AddWithValue("@curso", matricula.IdCurso);
        cmd.Parameters.AddWithValue("@data", matricula.DataMatricula);
        cmd.Parameters.AddWithValue("@ativa", matricula.Ativa);
        cmd.ExecuteNonQuery();
    }

    public void Atualizar(Matricula matricula)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        var cmd = new SqlCommand(
            "UPDATE Matricula SET DataMatricula=@data, Ativa=@ativa WHERE IdAluno=@aluno AND IdCurso=@curso", con);
        cmd.Parameters.AddWithValue("@aluno", matricula.IdAluno);
        cmd.Parameters.AddWithValue("@curso", matricula.IdCurso);
        cmd.Parameters.AddWithValue("@data", matricula.DataMatricula);
        cmd.Parameters.AddWithValue("@ativa", matricula.Ativa);
        cmd.ExecuteNonQuery();
    }

    public void Excluir(int idAluno, int idCurso)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        var cmd = new SqlCommand(
            "DELETE FROM Matricula WHERE IdAluno=@aluno AND IdCurso=@curso", con);
        cmd.Parameters.AddWithValue("@aluno", idAluno);
        cmd.Parameters.AddWithValue("@curso", idCurso);
        cmd.ExecuteNonQuery();
    }

    public Matricula Buscar(int idAluno, int idCurso)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        var cmd = new SqlCommand(
            "SELECT * FROM Matricula WHERE IdAluno=@aluno AND IdCurso=@curso", con);
        cmd.Parameters.AddWithValue("@aluno", idAluno);
        cmd.Parameters.AddWithValue("@curso", idCurso);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Matricula
            {
                IdAluno = (int)reader["IdAluno"],
                IdCurso = (int)reader["IdCurso"],
                DataMatricula = (DateTime)reader["DataMatricula"],
                Ativa = (bool)reader["Ativa"]
            };
        }
        return null;
    }

    public List<Matricula> BuscarTodos()
    {
        var lista = new List<Matricula>();
        using var con = new SqlConnection(_connectionString);
        con.Open();
        var cmd = new SqlCommand("SELECT * FROM Matricula", con);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new Matricula
            {
                IdAluno = (int)reader["IdAluno"],
                IdCurso = (int)reader["IdCurso"],
                DataMatricula = (DateTime)reader["DataMatricula"],
                Ativa = (bool)reader["Ativa"]
            });
        }
        return lista;
    }
}
