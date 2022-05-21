using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;

namespace OpentkProyect
{
    public partial class Form1 : Form
    {
        Escene currentScene;
        Objeto currentObjeto;
        Parte currentParte;

        List<Escene> escenarios = new List<Escene>();

        private int vertexBufferObject;
        private int vertexArrayObject;

        private IconButton currentBtn;
        private Panel leftBorderBtn;

        private IconButton currentBtn2;
        private Panel leftBorderBtn2;

        private IconButton currentBtn3;
        private Panel leftBorderBtn3;

        private int desplazarScene;
        private int desplazarObject;

        float position_x;
        float position_y;
        float position_z;
        float angulo;

        private struct RGBColors {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        public Form1() {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);

            leftBorderBtn2 = new Panel();
            leftBorderBtn2.Size = new Size(7, 60);

            leftBorderBtn3 = new Panel();
            leftBorderBtn3.Size = new Size(7, 30);

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            desplazarObject = 0;
            desplazarScene = 0;
            panelObjetos.Visible = false;
            panelPartes.Visible = false;

            currentScene = new Escene();
            position_x = 0.0f;
            position_y = 0.0f;
            position_z = 0.0f;
            angulo = 0.0f;
        }

        private void ActivateButton(object senderBtn, Color color) {
            if (senderBtn != null) {
                DisableButton();

                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(92, 84, 112);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }

        private void ActivateButtonObjeto(object senderBtn, Color color) {
            if (senderBtn != null) {
                DisableButtonObjeto();

                currentBtn2 = (IconButton)senderBtn;
                currentBtn2.BackColor = Color.FromArgb(92, 84, 112);
                currentBtn2.ForeColor = color;
                currentBtn2.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn2.IconColor = color;
                currentBtn2.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn2.ImageAlign = ContentAlignment.MiddleRight;

                leftBorderBtn2.BackColor = color;
                leftBorderBtn2.Location = new Point(10, currentBtn2.Location.Y);
                leftBorderBtn2.Visible = true;
                leftBorderBtn2.BringToFront();
            }
        }
        private void ActivateButtonParte(object senderBtn, Color color) {
            if (senderBtn != null)
            {
                DisableButtonParte();

                currentBtn3 = (IconButton)senderBtn;
                currentBtn3.BackColor = Color.FromArgb(92, 84, 112);
                currentBtn3.ForeColor = color;
                currentBtn3.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn3.IconColor = color;
                currentBtn3.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn3.ImageAlign = ContentAlignment.MiddleRight;

                leftBorderBtn3.BackColor = color;
                leftBorderBtn3.Location = new Point(10, currentBtn3.Location.Y);
                leftBorderBtn3.Visible = true;
                leftBorderBtn3.BringToFront();
            }

        }
        private void DisableButton() {
            if (currentBtn != null) {
                currentBtn.BackColor = Color.FromArgb(42, 36, 56);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;

                leftBorderBtn.Visible = false;
            }
        }

        private void DisableButtonObjeto() {
            if (currentBtn2 != null) {
                currentBtn2.BackColor = Color.FromArgb(42, 36, 56);
                currentBtn2.ForeColor = Color.Gainsboro;
                currentBtn2.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn2.IconColor = Color.Gainsboro;
                currentBtn2.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn2.ImageAlign = ContentAlignment.MiddleLeft;

                leftBorderBtn2.Visible = false;
            }
        }

        private void DisableButtonParte() { 
            if (currentBtn3 != null) {
                currentBtn3.BackColor = Color.FromArgb(42, 36, 56);
                currentBtn3.ForeColor = Color.Gainsboro;
                currentBtn3.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn3.IconColor = Color.Gainsboro;
                currentBtn3.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn3.ImageAlign = ContentAlignment.MiddleLeft;

                leftBorderBtn3.Visible = false;
            }       
        }
        private void GenerateSceneMenu() {
            panelScene.Controls.Clear();
            int altura = 0;
            foreach (Escene scene in escenarios) { 
                IconButton iconButtonCustom = new IconButton();
                panelScene.Controls.Add(iconButtonCustom);
                iconButtonCustom.Dock = DockStyle.None;
                iconButtonCustom.FlatAppearance.BorderSize = 0;
                iconButtonCustom.FlatStyle = FlatStyle.Flat;
                iconButtonCustom.IconChar = IconChar.ObjectGroup;
                iconButtonCustom.IconColor = Color.Gainsboro;
                iconButtonCustom.IconFont = IconFont.Auto;
                iconButtonCustom.IconSize = 30;
                iconButtonCustom.ImageAlign = ContentAlignment.MiddleLeft;
                iconButtonCustom.Location = new Point(0, altura);
                altura += 60;
                iconButtonCustom.Name = scene.name;
                iconButtonCustom.Padding = new Padding(12, 0, 20, 0);
                iconButtonCustom.Size = new Size(220, 60);
                iconButtonCustom.TabIndex = panelScene.Controls.Count + 1;
                iconButtonCustom.Text = scene.name;
                iconButtonCustom.TextAlign = ContentAlignment.MiddleLeft;
                iconButtonCustom.TextImageRelation = TextImageRelation.ImageBeforeText;
                iconButtonCustom.UseVisualStyleBackColor = true;
                iconButtonCustom.Click += (sender, e) => ClickScene(sender, RGBColors.color1, scene);
            }
        }
        private void GenerateObjectPanel(Escene scene, object sender) {
            panelObjetos.Controls.Clear();

            IconButton iconButton = (IconButton)sender;
            desplazarScene = scene.listObjeto.Count * 60;
            RepositionButtons(panelScene, iconButton.TabIndex, desplazarScene);
            int pos = iconButton.Location.Y + iconButton.Size.Height;
            panelScene.Controls.Add(panelObjetos);
            panelObjetos.Location = new Point(0, pos);
            panelObjetos.Name = scene.name;
            panelObjetos.Size = new Size(220, desplazarScene);

            int altura = 0;
            foreach (KeyValuePair<string, Objeto> k in scene.listObjeto) {
                IconButton iconButtonCustom = new IconButton();
                panelObjetos.Controls.Add(iconButtonCustom);
                iconButtonCustom.Dock = DockStyle.None;
                iconButtonCustom.FlatAppearance.BorderSize = 0;
                iconButtonCustom.FlatStyle = FlatStyle.Flat;
                iconButtonCustom.IconChar = IconChar.Cube;
                iconButtonCustom.IconColor = Color.Gainsboro;
                iconButtonCustom.IconFont = IconFont.Auto;
                iconButtonCustom.IconSize = 30;
                iconButtonCustom.ImageAlign = ContentAlignment.MiddleLeft;
                iconButtonCustom.Location = new Point(0, altura);
                altura += 60;
                iconButtonCustom.Name = k.Value.name;
                iconButtonCustom.Padding = new Padding(12, 0, 20, 0);
                iconButtonCustom.Size = new Size(220, 60);
                iconButtonCustom.TabIndex = panelObjetos.Controls.Count + 1;
                iconButtonCustom.Text = k.Value.name;
                iconButtonCustom.TextAlign = ContentAlignment.MiddleLeft;
                iconButtonCustom.TextImageRelation = TextImageRelation.ImageBeforeText;
                iconButtonCustom.UseVisualStyleBackColor = true;
                iconButtonCustom.Click += (sender, e) => ClickObjeto(sender, RGBColors.color1, k.Value);
            }

            panelObjetos.Visible = true;
        }

        private void GeneratePartPanel(Objeto objeto, object sender) {
            panelPartes.Controls.Clear();
            IconButton iconButton = (IconButton)sender;
            desplazarObject = objeto.listParte.Count * 30;
            RepositionButtons(panelScene, currentBtn.TabIndex, desplazarObject);
            RepositionButtons(panelObjetos, iconButton.TabIndex, desplazarObject);
            
            int pos = iconButton.Location.Y + iconButton.Size.Height;
            panelObjetos.Controls.Add(panelPartes);
            panelPartes.Location = new Point(0, pos);
            panelPartes.Name = objeto.name;
            panelPartes.Size = new Size(220, desplazarObject);

            panelObjetos.Size = new Size(220, panelObjetos.Size.Height + desplazarObject);

            int altura = 0;
            foreach (KeyValuePair<string, Parte> k in objeto.listParte) {
                IconButton iconButtonCustom = new IconButton();
                panelPartes.Controls.Add(iconButtonCustom);
                iconButtonCustom.Dock = DockStyle.None;
                iconButtonCustom.FlatAppearance.BorderSize = 0;
                iconButtonCustom.FlatStyle = FlatStyle.Flat;
                iconButtonCustom.IconChar = IconChar.DoorClosed;
                iconButtonCustom.IconColor = Color.Gainsboro;
                iconButtonCustom.IconFont = IconFont.Auto;
                iconButtonCustom.IconSize = 15;
                iconButtonCustom.ImageAlign = ContentAlignment.MiddleLeft;
                iconButtonCustom.Location = new Point(0, altura);
                altura += 30;
                iconButtonCustom.Name = k.Value.name;
                iconButtonCustom.Padding = new Padding(12, 0, 10, 0);
                iconButtonCustom.Size = new Size(220, 30);
                iconButtonCustom.TabIndex = panelPartes.Controls.Count + 1;
                iconButtonCustom.Text = k.Value.name;
                iconButtonCustom.TextAlign = ContentAlignment.MiddleLeft;
                iconButtonCustom.TextImageRelation = TextImageRelation.ImageBeforeText;
                iconButtonCustom.UseVisualStyleBackColor = true;
                iconButtonCustom.Click += (sender, e) => ClickParte(sender, RGBColors.color1, k.Value);
            }
            
            panelPartes.Visible = true;
        }

        private void CloseObjectPanel() {
            panelObjetos.Visible = false;
            RepositionButtons(panelScene, currentBtn.TabIndex, desplazarScene * -1);
        }

        private void ClosePartPanel() {
            panelPartes.Visible = false;
            RepositionButtons(panelObjetos, currentBtn2.TabIndex, desplazarObject * -1);
            RepositionButtons(panelScene, currentBtn.TabIndex, desplazarObject * -1);
        }
        private void RepositionButtons(Panel panel, int index, int altura) {
            IEnumerable<IconButton> iconButtons = panel.Controls.OfType<IconButton>();
            foreach (IconButton k in iconButtons) {
                if (k.TabIndex > index) {
                    k.Location = new Point(0, k.Location.Y + altura);
                }
            }
        
        }

        private void ClickScene(object senderBtn, Color color, Escene scene) {
            IconButton iconButton = (IconButton)senderBtn;
            if (panelPartes.Visible == true) {
                ClosePartPanel();
            }

            if (panelObjetos.Visible == true) {
                CloseObjectPanel();

            }
                
            if (currentBtn != null && currentBtn.Name == iconButton.Name) {
                DisableButton();
                currentBtn = null;
                currentScene = null;
                currentBtn2 = null;
                currentBtn3 = null;
                currentParte = null;
                currentObjeto = null;

            }
            else {
                panelScene.Controls.Add(leftBorderBtn);
                ActivateButton(senderBtn, color);
                GenerateObjectPanel(scene, senderBtn);
                currentScene = scene;
            }

            UpdatePanelControls(scene);
        }

        private void ClickObjeto(object senderBtn, Color color, Objeto objecto) {
            IconButton iconButton = (IconButton)senderBtn;
            if (panelPartes.Visible == true) {
                ClosePartPanel();
                currentBtn3 = null;
            }
                
            if (currentBtn2 != null && currentBtn2.Name == iconButton.Name) {
                DisableButtonObjeto();
                currentBtn2 = null;
                currentObjeto = null;
            }
            else {
                panelObjetos.Controls.Add(leftBorderBtn2);
                ActivateButtonObjeto(senderBtn, color);
                GeneratePartPanel(objecto, senderBtn);
                currentObjeto = objecto;
            }

            UpdatePanelControls(objecto);
        }
        private void ClickParte(object senderBtn, Color color, Parte parte) {
            IconButton iconButton = (IconButton)senderBtn;
            if (currentBtn3 != null && currentBtn3.Name == iconButton.Name) {
                DisableButtonParte();
                currentParte = null;
            }
            else {
                panelPartes.Controls.Add(leftBorderBtn3);
                ActivateButtonParte(senderBtn, color);
                currentParte = parte;
            }

            UpdatePanelControls(parte);
        }

        private void UpdatePanelControls(object  objeto) {
            if (objeto.GetType() == typeof(Escene)) {
                if (currentScene == null) {
                    name.Text = ".....";
                    clase.Text = ".....";
                }
                else {
                    name.Text = currentScene.name;
                    clase.Text = "Escene";
                }
            }
            else if(objeto.GetType() == typeof(Objeto)) {
                if (currentObjeto == null) {
                    name.Text = currentScene.name;
                    clase.Text = "Escene";
                }
                else {
                    name.Text = currentObjeto.name;
                    clase.Text = "Objeto";
                }
                
            }
            else {
                if (currentParte == null) {
                    name.Text = currentObjeto.name;
                    clase.Text = "Objeto";
                }
                else {
                    name.Text = currentParte.name;
                    clase.Text = "Parte";
                }
            }
            Render();
        }

        private void UpdateDataControls() {
            position_x = float.Parse(textBox1.Text, CultureInfo.InvariantCulture.NumberFormat);
            position_y = float.Parse(textBox2.Text, CultureInfo.InvariantCulture.NumberFormat);
            position_z = float.Parse(textBox3.Text, CultureInfo.InvariantCulture.NumberFormat);
            angulo = float.Parse(textBox4.Text, CultureInfo.InvariantCulture.NumberFormat);
        }
        


        //Abrir escenario.
        private void iconButton1_Click(object sender, EventArgs e) {
            panelMenu.Controls.Add(leftBorderBtn);
            if (panelObjetos.Visible == true) {
                CloseObjectPanel();
                name.Text = ".....";
                clase.Text = ".....";
                currentParte = null;
                currentObjeto = null;
                currentScene = null;
            }

            ActivateButton(sender, RGBColors.color1);
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                var path = openFileDialog1.FileName;
                Json json = new Json();
                Escene escene = json.deserializeEscene(path);
                escenarios.Add(escene);

                GenerateSceneMenu();
            }
            DisableButton();
        }
        private void iconButton2_Click(object sender, EventArgs e) {
            panelMenu.Controls.Add(leftBorderBtn);
            ActivateButton(sender, RGBColors.color2);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void glControl_MouseDown(object sender, MouseEventArgs e){
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /*
         * GL CONTROL OPENTK
         */
        private void glControl_Load(object? sender, EventArgs e){
            glControl.Resize += glControl_Resize;
            glControl.Paint += OnRender;
            
            GL.ClearColor(0.3607f, 0.3294f, 0.4392f, 1.0f);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);


            glControl_Resize(glControl, EventArgs.Empty);
        }
        private void glControl_Resize(object? sender, EventArgs e){
            glControl.MakeCurrent();

            if (glControl.ClientSize.Height == 0) {
                glControl.ClientSize = new System.Drawing.Size(glControl.ClientSize.Width, 1);
            }

            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
        }
        private void OnRender(object sender, PaintEventArgs e) {
            Render();
        }
        private void Render() {
            glControl.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.BindVertexArray(vertexArrayObject);

            if (currentScene == null) {
                currentScene = new Escene();
            }

            currentScene.Dibujar();

            glControl.SwapBuffers();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(vertexBufferObject);
            GL.DeleteVertexArray(vertexArrayObject);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateDataControls();
            if (currentParte != null) {
                currentParte.Escalar(position_x, position_y, position_z);
            }else if(currentObjeto != null) {
                currentObjeto.Escalar(position_x, position_y, position_z);
            }else if(currentScene != null) {
                currentScene.Escalar(position_x, position_y, position_z);
            }
            Render();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateDataControls();
            if (currentParte != null) {
                currentParte.Trasladar(position_x, position_y, position_z);
            }else if(currentObjeto != null) {
                currentObjeto.Trasladar(position_x, position_y, position_z);
            }else if(currentScene != null) {
                currentScene.Trasladar(position_x, position_y, position_z);
            }
            Render();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateDataControls();
            if (currentParte != null) {
                currentParte.Rotar(angulo, position_x, position_y, position_z);
            }else if(currentObjeto != null) {
                currentObjeto.Rotar(angulo, position_x, position_y, position_z);
            }else if(currentScene != null) {
                currentScene.Rotar(angulo, position_x, position_y, position_z);
            }
            Render();
        }
    }

}
