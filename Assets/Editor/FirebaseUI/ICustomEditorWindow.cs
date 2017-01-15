public interface ICustomEditorWindow
{
    bool requiresDatabase { get; set; }

    void Focus();
    void Draw();
}