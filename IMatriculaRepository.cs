
public interface IMatriculaRepository
{
    void Criar(Matricula matricula);
    void Atualizar(Matricula matricula);
    void Excluir(int idAluno, int idCurso);
    Matricula Buscar(int idAluno, int idCurso);
    List<Matricula> BuscarTodos();
}
