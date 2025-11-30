
public interface ICursoRepository
{
    void Criar(Curso curso);
    void Atualizar(Curso curso);
    void Excluir(int idCurso);
    Curso Buscar(int idCurso);
    List<Curso> BuscarTodos();
}
