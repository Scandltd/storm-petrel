namespace Test.Integration.WinFormsApp;

public partial class TestForm : Form
{
    public TestForm()
    {
        InitializeComponent();
        lblMain.Text = "Correct Text";
        lblMain.ForeColor = Color.Green;
    }
}
