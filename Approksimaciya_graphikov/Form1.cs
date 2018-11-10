using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms.DataVisualization.Charting;

namespace Approksimaciya_graphikov
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private Component component;
        private Data data = new Data();
        private List<Chart> charts = new List<Chart>();
        private List<List<int>> chartsCoordinates = new List<List<int>>();

        public int[] koordinaty_graphika = new int[250];
        public int temp = 0;
        int[] sdsfNormal = new int[250];
        int[] sdsfBigNormal = new int[250];
        int[] serbNormal = new int[250];
        int[] serb2Normal = new int[250];
        public int[] dsf = new int[250];
        public int[] dsf_bolshoy = new int[250];
        public int[] erb = new int[250];
        public int[] erb2 = new int[250];
        public int[] g652 = new int[250];
        public int[] g652_1 = new int[250];
        public int[] g652_2 = new int[250];
        public int[] nz_655_1 = new int[250];
        public int[] nz_g655_2 = new int[250];
        public int[] nz_g655_3 = new int[250];
        public int[] panda = new int[250];
        private int q = 0;



        public int[] mk200a = new int[250];





        public int dsf_average, dsf_bolshoy_average, erb_average, erb2_average, g652_average, g652_1_average, g652_2_average,
            nz_g655_1_average, nz_g655_2_average, nz_g655_3_average, panda_average, koordinaty_graphika_average;
        public int dsf_max, dsf_bolshoy_max, erb_max, erb2_max, g652_max, g652_1_max, g652_2_max, nz_g655_1_max, nz_g655_2_max,
            nz_g655_3_max, panda_max;
        public int aaaaa, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10;

        private void chart13_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        public int maximum, maxx;

        private void chart11_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void chart5_Click(object sender, EventArgs e)
        {

        }

        private void chart6_Click(object sender, EventArgs e)
        {

        }

        private void chart7_Click(object sender, EventArgs e)
        {

        }

        private void chart12_Click(object sender, EventArgs e)
        {

        }

        private void chart8_Click(object sender, EventArgs e)
        {

        }

        private void chart9_Click(object sender, EventArgs e)
        {

        }

        private void chart10_Click(object sender, EventArgs e)
        {

        }

        private void chart4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // диалог для выбора файла
            OpenFileDialog ofd = new OpenFileDialog();
            // фильтр форматов файлов
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            // если в диалоге была нажата кнопка ОК
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // загружаем изображение
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch // в случае ошибки выводим MessageBox
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) // если изображение в pictureBox1 имеется
            {
                // создаём Bitmap из изображения, находящегося в pictureBox1
                Bitmap input = new Bitmap(pictureBox1.Image);
                // создаём Bitmap для черно-белого изображения
                int j_start = 434, i_start = 453, j_end = input.Height - 22, i_end = input.Width - 97;
                Bitmap output = new Bitmap(i_end - i_start, j_end - j_start);
                // перебираем в циклах все пиксели исходного 
                //Image image = Image.Crop(new Rectangle(434, input.Height - 22, 453, input.Width - 97));
                for (int j = j_start; j < j_end; j++)
                    for (int i = i_start; i < i_end; i++)
                    {
                        // получаем (i, j) пиксель
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        // получаем компоненты цветов пикселя
                        float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                        float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                        float B = (float)(pixel & 0x000000FF); // синий
                        // делаем цвет черно-белым (оттенки серого) - находим среднее арифметическое
                        //R = G = B = (R + G + B) / 3.0f;
                        // собираем новый пиксель по частям (по каналам)
                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                        // добавляем его в Bitmap нового изображения
                        output.SetPixel(i - i_start, j - j_start, Color.FromArgb((int)pixel));
                    }
                // выводим черно-белый Bitmap в pictureBox2
                pictureBox2.Image = output;
                //textBox1.Text = ((UInt32)((input.GetPixel(500, 500).ToArgb()) & 0x00FF0000)/256/256).ToString() + " " + ((UInt32)((input.GetPixel(500, 500).ToArgb()) & 0x0000FF00)/256).ToString() + " " + ((UInt32)(input.GetPixel(500, 500).ToArgb()) & 0x000000FF).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null) // если изображение в pictureBox2 имеется
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить картинку как...";
                sfd.OverwritePrompt = true; // показывать ли "Перезаписать файл" если пользователь указывает имя файла, который уже существует
                sfd.CheckPathExists = true; // отображает ли диалоговое окно предупреждение, если пользователь указывает путь, который не существует
                // фильтр форматов файлов
                sfd.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                sfd.ShowHelp = true; // отображается ли кнопка Справка в диалоговом окне
                // если в диалоге была нажата кнопка ОК
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // сохраняем изображение
                        pictureBox2.Image.Save(sfd.FileName);
                    }
                    catch // в случае ошибки выводим MessageBox
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dsf_max = 0; dsf_bolshoy_max = 0; erb_max = 0; erb2_max = 0; g652_max = 0; g652_1_max = 0; g652_2_max = 0; nz_g655_1_max = 0; nz_g655_2_max = 0;
            nz_g655_3_max = 0; panda_max = 0;
            temp = 0;
            chart1.Series[0].Points.Clear();
            Bitmap input = new Bitmap(pictureBox2.Image);
            bool isBlack = false;
            // получаем (свободный пиксель, чтобы определить цвет фона графика
            UInt32 pixel = (UInt32)(input.GetPixel(0, input.Height - 1).ToArgb());
            // получаем компоненты цветов пикселя
            float R = (float)((pixel & 0x00FF0000) >> 16); // красный
            float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
            float B = (float)(pixel & 0x000000FF); // синий
            //если цвет свободного пикселя чёрный, то G=0, иначе G=255
            if (G == 0)
            {
                isBlack = true;
            }
            //Проверка: чёрный или нет? (см. методы ниже, рядом с поиском кореляции)
            if (isBlack)
            {
                grapfBlack(input, koordinaty_graphika);
            }
            else grapfWhite(input, koordinaty_graphika);
            ///// Тут начинается логика
            koordinaty_graphika[0] = koordinaty_graphika[1];
            //chart1.Series[0].Points.Clear();
            for (int i = 0; i < input.Width; i = i + 1)
            {
                //chart1.Series[0].Points.AddXY(i, koordinaty_graphika[i]);
                koordinaty_graphika_average += koordinaty_graphika[i];
                textBox1.Text = textBox1.Text + (koordinaty_graphika[i]).ToString() + ' ';
            }
            koordinaty_graphika_average = koordinaty_graphika_average / 250;
            for (int i = 0; i < input.Width; i = i + 1)
            {
                //koordinaty_graphika[i] = koordinaty_graphika[i] - koordinaty_graphika_average;
                chart1.Series[0].Points.AddXY(i, koordinaty_graphika[i]);
                // chart1.Series[0].Points.AddXY(i, serbNormal[i]);
            }
            component = new Component();
            component.setCoordinates(koordinaty_graphika); // Запоминаем график для дальнейшей сериализации

            List<double> correlationCoef = new List<double>();
            for (int i = 0; i < charts.Count; i++)
            {
                correlationCoef.Add(findCorrelation(koordinaty_graphika, chartsCoordinates[i].ToArray()));
            }
            //Метод который красит графики (см. ниже)
            coloringGraphs(correlationCoef);

            /* ГОВНОКОД

                for (int i = 0; i < input.Width; ++i)
                {
                    dsf_max = dsf_max + koordinaty_graphika[i] * dsf[i];
                    dsf_bolshoy_max = dsf_bolshoy_max + koordinaty_graphika[i] * dsf_bolshoy[i];
                    erb_max = erb_max + koordinaty_graphika[i] * erb[i];
                    erb2_max = erb2_max + koordinaty_graphika[i] * erb2[i];
                    g652_max = g652_max + koordinaty_graphika[i] * g652[i];
                    g652_1_max = g652_1_max + koordinaty_graphika[i] * g652_1[i];
                    g652_2_max = g652_2_max + koordinaty_graphika[i] * g652_2[i];
                    nz_g655_1_max = nz_g655_1_max + koordinaty_graphika[i] * nz_655_1[i];
                    nz_g655_2_max = nz_g655_2_max + koordinaty_graphika[i] * nz_g655_2[i];
                    nz_g655_3_max = nz_g655_3_max + koordinaty_graphika[i] * nz_g655_3[i];
                    panda_max = panda_max + koordinaty_graphika[i] * panda[i];
                }
            
              dsf_max = findCorrelation(koordinaty_graphika,sdsfNormal);
              dsf_bolshoy_max = dsf_bolshoy_max + koordinaty_graphika[i] * dsf_bolshoy[i];
              erb_max = erb_max + koordinaty_graphika[i] * erb[i];
              erb2_max = erb2_max + koordinaty_graphika[i] * erb2[i];
              g652_max = g652_max + koordinaty_graphika[i] * g652[i];
              g652_1_max = g652_1_max + koordinaty_graphika[i] * g652_1[i];
              g652_2_max = g652_2_max + koordinaty_graphika[i] * g652_2[i];
              nz_g655_1_max = nz_g655_1_max + koordinaty_graphika[i] * nz_655_1[i];
              nz_g655_2_max = nz_g655_2_max + koordinaty_graphika[i] * nz_g655_2[i];
              nz_g655_3_max = nz_g655_3_max + koordinaty_graphika[i] * nz_g655_3[i];
              panda_max = panda_max + koordinaty_graphika[i] * panda[i];
              

            maximum = dsf_max; maxx = 1;
            //if (maximum < dsf_bolshoy_max) { maximum = dsf_bolshoy_max; maxx = 2; }
            if (maximum < erb_max) { maximum = erb_max; maxx = 3; }
            if (maximum < erb2_max) { maximum = erb2_max; maxx = 4; }
            if (maximum < g652_max) { maximum = g652_max; maxx = 5; }
            if (maximum < g652_1_max) { maximum = g652_1_max; maxx = 6; }
            if (maximum < g652_2_max) { maximum = g652_2_max; maxx = 7; }
            if (maximum < nz_g655_1_max) { maximum = nz_g655_1_max; maxx = 8; }
            if (maximum < nz_g655_2_max) { maximum = nz_g655_2_max; maxx = 9; }
            if (maximum < nz_g655_3_max) { maximum = nz_g655_3_max; maxx = 10; }
            if (maximum < panda_max) { maximum = panda_max; maxx = 11; }
            chart2.BackColor = Color.Red;
            chart3.BackColor = Color.Brown;
            chart4.BackColor = Color.Red;
            chart5.BackColor = Color.Red;
            chart6.BackColor = Color.Red;
            chart7.BackColor = Color.Red;
            chart8.BackColor = Color.Red;
            chart9.BackColor = Color.Red;
            chart10.BackColor = Color.Red;
            chart11.BackColor = Color.Red;
            chart12.BackColor = Color.Red;
            chart13.BackColor = Color.Red;
            chart14.BackColor = Color.Red;
            chart15.BackColor = Color.Red;
            chart16.BackColor = Color.Red;
            chart17.BackColor = Color.Red;
            chart18.BackColor = Color.Red;
            chart19.BackColor = Color.Red;
            chart20.BackColor = Color.Red;
            chart21.BackColor = Color.Red;
            textBox3.Text = (dsf_max).ToString();
            //textBox4.Text = (dsf_bolshoy_max).ToString();
            textBox5.Text = erb_max.ToString();
            textBox6.Text = (erb2_max).ToString();
            textBox7.Text = (g652_max).ToString();
            textBox8.Text = (g652_1_max).ToString();
            textBox9.Text = (g652_2_max).ToString();
            textBox10.Text = (nz_g655_1_max).ToString();
            textBox11.Text = (nz_g655_2_max).ToString();
            textBox12.Text = (nz_g655_3_max).ToString();
            textBox13.Text = (panda_max).ToString();
            textBox25.Text = "200mk";
            textBox27.Text = "DSF_2";
            textBox29.Text = "erb C";
            textBox31.Text = "erb G";
            textBox33.Text = "LBL";
            textBox35.Text = "LEAF";
            textBox37.Text = "Panda_2";
            textBox39.Text = "ultra";


            if (maxx == 1) { chart2.BackColor = Color.PaleGreen; textBox1.Text = "dsf"; }//////////////////////////////////////////////////////////////
            //if (maxx == 2) { chart3.BackColor = Color.PaleGreen; textBox1.Text = "erb"; }
            if (maxx == 3) { chart4.BackColor = Color.PaleGreen; textBox1.Text = "erb"; }
            if (maxx == 4) { chart5.BackColor = Color.PaleGreen; textBox1.Text = "erb2"; }
            if (maxx == 5) { chart6.BackColor = Color.PaleGreen; textBox1.Text = "g652"; }
            if (maxx == 6) { chart7.BackColor = Color.PaleGreen; textBox1.Text = "g652_1"; }
            if (maxx == 7) { chart8.BackColor = Color.PaleGreen; textBox1.Text = "g652_2"; }
            if (maxx == 8) { chart9.BackColor = Color.PaleGreen; textBox1.Text = "nz_g655_1"; }
            if (maxx == 9) { chart10.BackColor = Color.PaleGreen; textBox1.Text = "nz_g655_2"; }
            if (maxx == 10) { chart11.BackColor = Color.PaleGreen; textBox1.Text = "nz_g655_3"; }
            if (maxx == 11) { chart12.BackColor = Color.PaleGreen; textBox1.Text = "panda"; }






            if (dsf_max < maximum && dsf_max > 0.85 * maximum) { chart2.BackColor = Color.Yellow; }
            if (erb_max < maximum && erb_max > 0.85 * maximum) { chart4.BackColor = Color.Yellow; }
            if (erb2_max < maximum && erb2_max > 0.85 * maximum) { chart5.BackColor = Color.Yellow; }
            if (g652_max < maximum && g652_max > 0.85 * maximum) { chart6.BackColor = Color.Yellow; }
            if (g652_1_max < maximum && g652_1_max > 0.85 * maximum) { chart7.BackColor = Color.Yellow; }
            if (g652_2_max < maximum && g652_2_max > 0.85 * maximum) { chart8.BackColor = Color.Yellow; }
            if (nz_g655_1_max < maximum && nz_g655_1_max > 0.85 * maximum) { chart9.BackColor = Color.Yellow; }
            if (nz_g655_2_max < maximum && nz_g655_2_max > 0.85 * maximum) { chart10.BackColor = Color.Yellow; }
            if (nz_g655_3_max < maximum && nz_g655_3_max > 0.85 * maximum) { chart11.BackColor = Color.Yellow; }
            if (panda_max < maximum && panda_max > 0.85 * maximum) { chart12.BackColor = Color.Yellow; }

            if (dsf_max < 0.85 * maximum && dsf_max > 0.75 * maximum) { chart2.BackColor = Color.Orange; }
            if (erb_max < 0.85 * maximum && erb_max > 0.75 * maximum) { chart4.BackColor = Color.Orange; }
            if (erb2_max < 0.85 * maximum && erb2_max > 0.75 * maximum) { chart5.BackColor = Color.Orange; }
            if (g652_max < 0.85 * maximum && g652_max > 0.75 * maximum) { chart6.BackColor = Color.Orange; }
            if (g652_1_max < 0.85 * maximum && g652_1_max > 0.75 * maximum) { chart7.BackColor = Color.Orange; }
            if (g652_2_max < 0.85 * maximum && g652_2_max > 0.75 * maximum) { chart8.BackColor = Color.Orange; }
            if (nz_g655_1_max < 0.85 * maximum && nz_g655_1_max > 0.75 * maximum) { chart9.BackColor = Color.Orange; }
            if (nz_g655_2_max < 0.85 * maximum && nz_g655_2_max > 0.75 * maximum) { chart10.BackColor = Color.Orange; }
            if (nz_g655_3_max < 0.85 * maximum && nz_g655_3_max > 0.75 * maximum) { chart11.BackColor = Color.Orange; }
            if (panda_max < 0.85 * maximum && panda_max > 0.75 * maximum) { chart12.BackColor = Color.Orange; }
             */
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Перегруженная функция , выполняется каждый раз, когда обновляется форма примерно раз в секунду, поэтому снизу q==0, чтобы выполнилось 1 раз
            base.OnPaint(e);

            if (q == 0)
            {
                createGrafics();
            }
            q++;
        }

        private void createGrafics()
        {
            /// Код ниже срабатывает при запуске программы
            Deserialize();
            int graficsCount = (data.data.Count);
            int shiftX = 0;
            int shiftY = 0;
            // Ниже логика отрисовки графиков
            for (int i = 0; i < graficsCount; i++)
            {
                if (i % 4 == 0 && i != 0)
                {
                    shiftX += 190;
                    shiftY = 0;
                }
                Point startLocation = new Point(16 + shiftY, 388 + shiftX); // 16 388
                shiftY += 310;
                ChartArea chartArea = new ChartArea();
                Chart myChart = new Chart();
                chartArea.Position = new ElementPosition(0, 0, 100, 100);
                myChart.ChartAreas.Add(chartArea);
                myChart.Location = startLocation;
                myChart.Size = new Size(300, 180);
                myChart.Visible = true;
                myChart.Parent = this;
                myChart.CreateControl();
                // myChart.BackColor = Color.Green;
                charts.Add(myChart);
                myChart.Series.Add(new Series());
                // myChart.Series[0].Points.Clear();
                myChart.Series[0].Enabled = true;
                myChart.Enabled = true;

                List<int> coordinates = new List<int>();
                for (int j = 0; j < 250; j++)
                {
                    myChart.Series[0].Points.AddXY(j, data.data[i].coordinates[j]);
                    coordinates.Add(data.data[i].coordinates[j]);
                    // data экземплял класса, закруженного из файла при запуске -- функция Deserialize()
                }
                chartsCoordinates.Add(coordinates);
            }
        }

        private int[] grapfBlack(Bitmap input, int[] koordinaty_graphika)
        {
            for (int j = 0; j < input.Width; j++)
            {
                for (int i = 0; i < input.Height; i++)
                {
                    // получаем (i, j) пиксель
                    UInt32 pixel = (UInt32)(input.GetPixel(j, i).ToArgb());
                    // получаем компоненты цветов пикселя
                    float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                    float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                    float B = (float)(pixel & 0x000000FF); // синий
                    if ((G == 51) && (temp == 0) && (j == 0) || (G == 51) && (temp != 0) && (j != 0))
                    {
                        temp = 1;
                        koordinaty_graphika[j] = 140 - i;
                        //chart1.Series[0].Points.AddXY(j, koordinaty_graphika[j]);
                        //    textBox1.Text = textBox1.Text + (koordinaty_graphika[j]).ToString() + ' ';
                        break;
                    }
                }
            }

            return koordinaty_graphika;
        }

        private int[] grapfWhite(Bitmap input, int[] koordinaty_graphika)
        {
            for (int j = 0; j < input.Width; j++)
            {
                for (int i = input.Height - 1; i > 0; i--)
                {
                    // получаем (i, j) пиксель
                    UInt32 pixel = (UInt32)(input.GetPixel(j, i).ToArgb());
                    // получаем компоненты цветов пикселя
                    float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                    float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                    float B = (float)(pixel & 0x000000FF); // синий

                    if ((G == 0) && (temp == 0) && (j % 10 != 0))
                    // if ((G == 0) && (temp == 0))
                    {
                        koordinaty_graphika[j] = 140 - i;
                        //  koordinaty_graphika[j] = i;
                        //chart1.Series[0].Points.AddXY(j, koordinaty_graphika[j]);
                        break;
                    }

                }

                if ((j % 10 == 1) && (j > 10))
                {
                    koordinaty_graphika[j - 1] = (koordinaty_graphika[j] + koordinaty_graphika[j - 2]) / 2;
                }
            }
            return koordinaty_graphika;
        }

        private double findCorrelation(int[] x, int[] y)
        {
            int xAverage = 0;
            for (int i = 0; i < x.Length; i++)
            {
                xAverage += x[i];
            }
            xAverage = xAverage / x.Length;
            int yAverage = 0;
            for (int i = 0; i < y.Length; i++)
            {
                yAverage += y[i];
            }
            yAverage = yAverage / y.Length;
            double correlationNumerator = 0;
            for (int i = 0; i < x.Length; i++)
            {
                correlationNumerator += (x[i] - xAverage) * (y[i] - yAverage);
            }
            double correlationDenominator = 0;
            for (int i = 0; i < x.Length; i++)
            {
                correlationDenominator += (Math.Pow(x[i] - xAverage, 2)) * (Math.Pow(y[i] - yAverage, 2));
            }
            correlationDenominator = Math.Sqrt(correlationDenominator);
            double correlation = correlationNumerator / correlationDenominator;
            //   return correlation;
            return correlationNumerator;
        }

        private void coloringGraphs(List<double> correlationCoef)
        {
            //   int[] firstThreeMax = new int[3];   //массив позиций с максимальными кореляциями в порядке убывания
            double[] tempCorrelation = new double[correlationCoef.Count]; //временный массив кореляций с которым работаем
            bool[] checkBoolean = new bool[] { false, false, false, false }; //Была ли записана корреляция? (можно сделать через notNull в массиве firstThreeMax)

            for (int i = 0; i < correlationCoef.Count; i++)
            {
                tempCorrelation[i] = correlationCoef[i];
            }
            Array.Sort(tempCorrelation);
            Array.Reverse(tempCorrelation);

            for (int i = 0; i < correlationCoef.Count; i++)
            {
                if (correlationCoef[i] == tempCorrelation[0] && !checkBoolean[0])
                {
                    charts[i].BackColor = Color.Green;
                    checkBoolean[0] = true;
                    //     firstThreeMax[0] = i;
                }
                else if (correlationCoef[i] == tempCorrelation[1] && !checkBoolean[1])
                {
                    charts[i].BackColor = Color.Orange;
                    checkBoolean[1] = true;
                    //  firstThreeMax[1] = i;
                }
                else if (correlationCoef[i] == tempCorrelation[2] && !checkBoolean[2])
                {
                    checkBoolean[2] = true;
                    charts[i].BackColor = Color.Orange;
                    //firstThreeMax[2] = i;
                }
                else charts[i].BackColor = Color.Red;
            }
            //for (int i = 0; i < charts.Count; i++)
            //{
            //  if (i == firstThreeMax[0])
            //{
            //  charts[i].BackColor = Color.Green;
            //}
            //else if (i == firstThreeMax[1] | i == firstThreeMax[2])
            //{
            //  charts[i].BackColor = Color.Orange;
            //}
            //else charts[i].BackColor = Color.Red;
            //           }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.data.addComponent(component); // Кнопка добавить
        }

        private void button6_Click(object sender, EventArgs e) // Кнопка серилизовать
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("./data.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this.data);
            }

        }

        private void Deserialize()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("./data.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    data = (Data)formatter.Deserialize(fs);
                }
                catch (Exception e)
                {
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            //  this.Width = SystemInformation.PrimaryMonitorSize.Width / 3;
            //     this.Height = SystemInformation.PrimaryMonitorSize.Height / 3;
            //ГОВНОКОД ->
            /*
            textBox1.Text = Convert.ToString(data.data.Count);
            int[] sdsfNormal = new int[250];
            String sdsf = "41 43 45 47 49 51 51 51 52 52 52 53 54 55 56 57 59 60 61 63 64 66 68 69 71 73 74 " +
                "76 77 78 80 81 83 85 87 89 90 91 92 92 93 94 94 94 94 94 94 93 93 92 92 91 90 89 87 86 85 84 82" +
                " 81 80 78 77 75 73 72 70 70 69 69 68 68 68 68 68 67 67 67 68 68 69 69 70 71 72 73 74 75 76 78 79 80 " +
                "81 81 82 82 83 83 83 83 83 83 83 82 81 80 79 78 77 77 76 76 75 74 73 73 72 71 71 71 72 72 72 73 74 76 77" +
                " 78 79 81 82 83 85 86 87 89 90 91 92 92 93 93 94 94 94 94 94 94 94 93 92 91 90 89 88 87 86 85 84 83 81 80" +
                " 79 77 76 76 75 75 74 73 72 72 71 70 69 69 68 67 67 66 65 64 64 63 62 62 61 61 60 60 59 57 56 55 53 53 53" +
                " 52 52 52 52 52 52 52 52 52 52 51 51 51 50 50 49 49 48 47 46 45 44 43 44 44 45 45 46 45 44 43 42 41 40 38" +
                " 35 32 29 26 26 27 28 29 30 30 31 31 32 32 30 28 26 24 ";
            String[] w = sdsf.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                sdsfNormal[i] = Convert.ToInt16(w[i]);
                dsf[i] = Convert.ToInt16(w[i]);
                dsf_average = dsf_average + dsf[i];
            }
            dsf_average = dsf_average / 250;

            String sdsf_bolshoy = "41 43 45 47 49 51 51 51 52 52 52 53 54 55 56 57 59 60 61 63 64 66 68 69 71 73 74 " +
                "76 77 78 80 81 83 85 87 89 90 91 92 92 93 94 94 94 94 94 94 93 93 92 92 91 90 89 87 86 85 84 82" +
                " 81 80 78 77 75 73 72 70 70 69 69 68 68 68 68 68 67 67 67 68 68 69 69 70 71 72 73 74 75 76 78 79 80 " +
                "81 81 82 82 83 83 83 83 83 83 83 82 81 80 79 78 77 77 76 76 75 74 73 73 72 71 71 71 72 72 72 73 74 76 77" +
                " 78 79 81 82 83 85 86 87 89 90 91 92 92 93 93 94 94 94 94 94 94 94 93 92 91 90 89 88 87 86 85 84 83 81 80" +
                " 79 77 76 76 75 75 74 73 72 72 71 70 69 69 68 67 67 66 65 64 64 63 62 62 61 61 60 60 59 57 56 55 53 53 53" +
                " 52 52 52 52 52 52 52 52 52 52 51 51 51 50 50 49 49 48 47 46 45 44 43 44 44 45 45 46 45 44 43 42 41 40 38" +
                " 35 32 29 26 26 27 28 29 30 30 31 31 32 32 30 28 26 24 ";
            w = sdsf_bolshoy.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                sdsfBigNormal[i] = Convert.ToInt16(w[i]);
                dsf_bolshoy[i] = Convert.ToInt16(w[i]);
                dsf_bolshoy_average = dsf_bolshoy_average + dsf_bolshoy[i];
                dsf[i] -= dsf_average;
                aaaaa += Math.Abs(dsf[i]);
            }
            aaaaa /= 250;
            textBox2.Text = (aaaaa).ToString();
            dsf_bolshoy_average = dsf_bolshoy_average / 250;

            String serb = "79 79 80 80 81 81 82 82 83 83 83 84 85 85 85 86 86 87 87 87 87 88 88 89 89 89 89 90 90 90 90 91" +
                " 91 91 92 92 92 92 92 92 92 93 93 93 93 93 93 93 93 93 93 93 93 93 92 92 92 92 91 91 91 91 90 90 90 89 89" +
                " 89 89 88 88 88 87 87 87 87 86 86 86 86 85 85 85 85 84 84 83 83 82 82 81 81 80 80 79 79 78 78 78 78 77 77" +
                " 77 76 76 76 75 75 75 74 73 73 73 73 72 72 72 72 71 71 71 71 71 70 70 70 70 69 69 68 68 68 67 67 66 66 66" +
                " 66 65 65 65 65 65 65 64 64 64 63 63 62 61 61 60 60 59 59 58 58 57 57 57 57 56 56 56 56 55 55 54 53 52 51" +
                " 51 50 49 48 47 46 47 47 48 49 49 50 50 51 52 52 53 53 53 53 53 53 52 52 52 52 52 52 52 52 52 52 51 51 51" +
                " 51 51 51 50 50 49 49 48 48 47 47 46 46 45 45 45 44 44 44 43 43 42 42 43 44 45 46 47 48 49 50 51 52 52 53" +
                " 53 53 53 53 53 53 53 53 ";
            w = serb.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {

                serbNormal[i] = Convert.ToInt16(w[i]);
                //  chart1.Series[0].Points.AddXY(i, serbNormal[i]);
                erb[i] = Convert.ToInt16(w[i]);
                erb_average = erb_average + erb[i];
                dsf_bolshoy[i] -= dsf_bolshoy_average;
                a1 += Math.Abs(dsf_bolshoy[i]);
            }
            a1 /= 250;
            erb_average = erb_average / 250;

            String serb2 = "81 81 81 82 83 83 84 85 86 86 86 87 88 88 88 89 89 90 90 90 90 91 91 91 92 92 92 92 92 93 93 93" +
                " 93 93 94 94 94 94 94 94 94 95 95 95 95 95 95 95 94 94 94 94 94 94 94 94 94 94 94 94 94 94 94 94 94 93 93 92" +
                " 92 92 91 91 90 90 90 90 89 89 89 89 88 88 88 88 87 87 86 86 85 85 84 84 83 83 82 82 81 81 80 80 79 79 78 78" +
                " 77 77 76 76 76 75 74 74 74 74 73 73 73 72 72 72 71 71 71 71 70 70 70 69 69 68 68 68 67 67 66 66 65 65 64 64" +
                " 63 63 62 62 61 61 60 60 60 60 60 60 61 61 61 61 61 61 61 60 60 60 60 60 60 59 59 59 59 59 59 59 59 60 60 60" +
                " 60 60 60 60 60 60 60 59 59 59 59 59 59 58 57 57 56 55 54 53 53 52 51 51 50 50 50 50 49 49 49 48 48 48 48 49" +
                " 49 50 50 51 51 51 52 52 51 51 51 51 51 50 50 50 49 49 49 50 50 51 51 52 52 53 53 54 54 54 53 53 52 52 52 51" +
                " 51 50 ";
            w = serb2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                serb2Normal[i] = Convert.ToInt16(w[i]);
                erb2[i] = Convert.ToInt16(w[i]);
                erb2_average = erb2_average + erb2[i];
                erb[i] -= erb_average;
                a2 += Math.Abs(erb[i]);
                dsf_bolshoy[i] = dsf_bolshoy[i] * aaaaa / a1;

            }
            a2 /= 250;
            erb2_average = erb2_average / 250;

            String sg652 = "60 60 60 60 60 60 61 61 61 61 61 61 62 62 62 62 63 63 63 63 63 64 65 65 66 67 67 68 69 70 70 71 72" +
                " 72 73 73 74 74 75 75 75 76 77 77 78 78 79 79 79 80 80 81 81 81 82 82 82 83 83 84 84 84 85 85 86 86 87 87 88 89" +
                " 89 90 90 91 91 92 92 92 92 93 93 93 94 94 94 95 95 95 95 96 96 96 96 97 97 97 98 98 98 98 98 99 99 100 100 100" +
                " 100 100 100 100 100 101 101 101 101 101 101 101 101 101 100 100 100 100 100 100 100 100 99 99 99 99 99 98 98 98" +
                " 98 97 97 97 96 96 96 96 96 95 95 95 94 94 93 93 93 92 92 91 91 91 90 90 90 90 89 89 89 89 88 88 88 87 86 86 86" +
                " 86 85 85 84 84 83 83 82 82 81 81 80 80 79 79 78 78 77 77 77 77 76 76 76 75 75 75 74 74 73 73 72 72 71 71 70 70" +
                " 70 70 70 70 70 70 70 70 70 70 70 70 69 69 69 69 69 68 68 68 68 68 68 68 68 68 68 68 68 68 68 68 67 67 66 66 65" +
                " 65 64 64 ";
            w = sg652.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                g652[i] = Convert.ToInt16(w[i]);
                g652_average = g652_average + g652[i];
                erb2[i] -= erb2_average;
                a3 += Math.Abs(erb2[i]);
                erb2[i] = erb2[i] * aaaaa / a2;

            }
            a3 /= 250;
            g652_average = g652_average / 250;

            String sg652_1 = "38 38 38 38 38 38 38 38 38 35 29 23 17 11 5 -1 0 2 4 6 8 10 12 15 20 24 29 " +
                "34 38 43 46 44 42 40 38 36 34 32 31 33 36 38 40 43 45 47 48 48 49 49 50 51 51 52 52 52 " +
                "51 51 51 51 50 50 49 48 47 45 44 43 42 44 46 47 49 50 52 54 55 56 58 59 60 62 63 64 64 " +
                "64 64 64 65 65 65 65 65 65 65 66 66 66 66 67 68 69 70 71 72 73 74 75 75 76 77 78 78 79 " +
                "80 80 81 81 82 82 83 83 84 84 85 85 86 87 87 88 89 89 90 90 91 91 92 92 93 93 93 94 94 " +
                "95 95 95 95 95 95 95 95 95 95 95 95 95 94 94 94 94 94 94 94 94 93 93 93 93 93 92 92 92 " +
                "92 91 91 90 90 89 89 88 87 87 86 86 85 85 85 85 84 84 84 83 83 82 82 81 81 80 79 78 77 " +
                "76 75 74 73 72 72 72 72 73 73 73 73 72 72 71 71 70 69 69 68 67 66 65 65 64 63 62 62 62 " +
                "62 62 62 62 62 62 62 61 61 60 60 59 59 59 60 60 61 61 62 62 ";
            w = sg652_1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < w.Length; ++i)
            {
                g652_1[i] = Convert.ToInt16(w[i]);
                g652_1_average += g652_1[i];
                g652[i] -= g652_average;
                a4 += Math.Abs(g652[i]);
                erb2[i] = erb2[i] * aaaaa / a3;

            }
            a4 /= 250;
            g652_1_average = g652_1_average / 250;

            String sg652_2 = "24 26 28 30 32 34 36 38 40 40 39 39 39 39 38 38 37 37 36 35 34 34 33 32 33 " +
                "34 35 36 37 38 39 39 39 38 38 38 38 37 37 36 34 33 31 29 28 27 29 31 33 35 37 39 41 43 " +
                "45 47 48 50 51 53 55 56 56 56 56 56 56 56 56 56 56 56 56 55 55 55 55 56 58 59 60 62 63 " +
                "64 64 65 65 66 66 67 67 68 69 69 70 71 72 72 73 73 73 73 73 72 72 72 72 73 73 74 74 75 " +
                "76 76 77 78 79 81 82 83 84 85 85 86 86 86 87 87 88 88 89 89 90 90 91 91 92 92 93 93 93 " +
                "94 94 95 95 95 95 95 95 95 95 95 95 95 95 95 95 95 95 95 95 95 95 95 94 94 94 94 94 93 " +
                "93 92 92 91 91 91 90 90 89 89 88 88 87 86 86 85 85 84 84 83 83 82 82 82 81 81 80 80 79 " +
                "78 77 77 76 75 74 73 72 72 71 71 70 70 69 69 69 70 70 70 70 71 71 71 70 70 69 69 68 68 " +
                "67 67 66 65 64 64 63 62 62 62 62 61 61 61 61 61 61 61 61 61 61 61 ";
            w = sg652_2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                g652_2[i] = Convert.ToInt16(w[i]);
                g652_2_average = g652_2_average + g652_2[i];
                g652_1[i] -= g652_1_average;
                a5 += Math.Abs(g652_1[i]);
                g652[i] = g652[i] * aaaaa / a4;

            }
            a5 /= 250;
            g652_2_average = g652_2_average / 250;

            String snz_655_1 = "48 49 49 50 50 51 52 52 53 54 54 55 55 56 56 57 57 58 58 59 59 60 60 61 " +
                "61 60 60 60 60 59 59 60 60 61 62 63 63 64 65 66 67 68 68 69 70 71 72 72 73 74 75 75 76 " +
                "77 77 78 78 79 79 80 80 81 82 82 83 83 84 84 85 85 85 85 85 86 86 86 86 86 87 87 87 87 " +
                "88 88 88 88 88 88 88 88 88 88 88 88 88 89 89 89 89 89 88 88 88 87 87 86 86 85 85 84 84 " +
                "83 82 82 81 81 80 80 79 79 78 78 78 77 77 77 76 76 75 75 75 74 74 74 74 73 73 73 74 74 " +
                "74 75 75 76 76 76 76 76 76 75 75 75 75 75 75 75 75 75 75 75 75 75 76 76 76 76 77 77 77 " +
                "77 77 76 76 76 76 76 76 75 75 75 75 74 74 74 73 73 73 73 72 72 72 71 71 71 70 70 69 69 " +
                "69 69 69 69 69 69 69 69 69 68 68 67 67 66 66 66 67 67 67 68 68 69 69 69 68 68 67 67 66 " +
                "66 67 67 68 69 70 70 71 72 72 72 72 71 71 71 71 71 71 70 70 70 70 69 ";
            w = snz_655_1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                nz_655_1[i] = Convert.ToInt16(w[i]);
                nz_g655_1_average = nz_g655_1_average + nz_655_1[i];
                g652_2[i] -= g652_2_average;
                a6 += Math.Abs(g652_2[i]);
                g652_1[i] = g652_1[i] * aaaaa / a5;

            }
            a6 /= 250;
            nz_g655_1_average = nz_g655_1_average / 250;

            String snz_655_2 = "39 39 41 43 45 47 49 51 52 53 54 56 55 54 53 52 51 51 53 56 58 61 63 63 64 64 65 65 66 68" +
                " 34 0 73 74 75 75 76 77 77 78 79 81 82 83 84 84 84 84 85 85 85 86 86 87 88 88 89 89 89 89 89 89 89 89 89" +
                " 90 90 90 90 90 89 89 89 89 88 88 87 87 86 85 84 84 84 83 83 82 82 81 81 80 79 78 78 77 76 75 74 74 73" +
                " 72 36 0 72 72 72 73 73 73 73 74 74 74 74 74 74 75 75 75 76 76 76 77 78 78 77 77 77 76 76 76 76 76 76 76" +
                " 75 74 74 73 72 0 0 0 72 72 72 72 0 0 70 70 69 69 69 69 70 70 70 70 70 70 70 70 70 70 70 70 70 70 0 72 72" +
                " 73 74 74 73 73 73 72 72 0 34 68 67 66 66 66 66 67 67 67 66 66 66 65 65 65 65 65 65 65 64 64 63 63 62 61" +
                " 60 60 59 59 58 58 57 56 55 53 52 51 53 55 56 58 59 57 55 53 51 49 47 45 43 41 39 37 38 38 39 40 40 41 41" +
                " 42 43 43 44 43 41 40 39 37 ";
            w = snz_655_2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                nz_g655_2[i] = Convert.ToInt16(w[i]);
                nz_g655_2_average = nz_g655_2_average + nz_g655_2[i];
                nz_655_1[i] -= nz_g655_1_average;
                a7 += Math.Abs(nz_655_1[i]);
                g652_2[i] = g652_2[i] * aaaaa / a6;


            }
            a7 /= 250;
            nz_g655_2_average = nz_g655_2_average / 250;

            String snz_g655_3 = "49 49 50 51 51 52 0 54 55 55 55 56 57 57 58 58 58 57 57 57 56 56 55 55 56 57 58 59 60 61 62 " +
                "63 63 64 65 66 66 67 68 69 69 70 71 72 72 73 74 74 75 75 76 77 77 78 78 79 79 80 80 81 81 82 82 82 82 83 83 " +
                "83 83 84 84 85 85 86 87 87 88 88 88 88 88 89 89 89 89 89 89 89 88 88 88 88 88 88 88 87 87 87 87 87 86 86 86 " +
                "86 86 85 85 85 84 84 83 83 83 82 82 82 81 81 81 81 80 80 80 79 79 78 78 77 77 76 76 76 76 77 77 77 77 77 76 " +
                "76 75 75 74 74 73 73 73 73 73 74 74 74 74 74 74 74 75 75 75 75 75 76 76 77 77 78 78 79 79 78 78 78 78 77 77 " +
                "76 76 75 75 74 73 73 72 72 73 73 74 74 75 75 74 74 73 73 72 71 71 70 70 69 68 68 68 67 67 66 66 67 67 67 67 " +
                "68 68 68 68 68 68 67 67 67 67 68 68 69 70 71 71 72 72 72 72 72 71 71 71 71 71 70 70 70 69 69 69 69 69 69 69 " +
                "70 70 70 ";
            w = snz_g655_3.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                nz_g655_3[i] = Convert.ToInt16(w[i]);
                nz_g655_3_average = nz_g655_3_average + nz_g655_3[i];
                nz_g655_2[i] = nz_g655_2[i] - nz_g655_2_average;
                a8 += Math.Abs(nz_g655_2[i]);
                nz_655_1[i] = nz_655_1[i] * aaaaa / a7;

            }
            a8 /= 250;
            nz_g655_3_average = nz_g655_3_average / 250;

            String spanda = "58 58 59 59 60 60 61 61 62 62 62 63 63 63 64 64 64 64 65 65 65 65 66 66 66 66 67 67 67 67 67 67" +
                " 68 68 68 68 68 69 69 69 69 69 69 70 70 70 70 70 70 71 71 71 71 71 71 72 72 72 72 72 72 73 73 73 73 73 74 74" +
                " 74 74 74 74 75 75 75 75 75 75 76 76 76 76 76 77 77 77 77 77 77 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78" +
                " 78 78 79 79 79 79 79 79 80 80 80 80 80 80 80 80 80 81 81 81 81 81 81 81 81 81 81 81 81 81 81 81 81 81 81 81" +
                " 81 81 81 82 82 82 82 82 82 82 82 82 82 82 81 81 81 81 81 81 81 81 81 81 81 82 82 82 82 82 82 82 82 82 81 81" +
                " 81 81 81 81 80 80 80 80 80 80 80 80 79 79 79 79 79 79 79 79 79 79 79 79 79 79 79 79 79 79 79 78 78 78 77 77" +
                " 76 76 76 75 75 75 75 75 75 75 74 74 74 74 74 74 74 74 73 73 73 73 72 72 72 72 71 71 71 71 70 70 70 70 70 70" +
                " 69 69 ";
            w = spanda.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                panda[i] = Convert.ToInt16(w[i]);
                panda_average = panda_average + panda[i];
                nz_g655_3[i] = nz_g655_3[i] - nz_g655_3_average;
                a9 += Math.Abs(nz_g655_3[i]);
                nz_g655_2[i] = nz_g655_2[i] * aaaaa / a8;

            }
            panda_average = panda_average / 250;
            a9 /= 250;
            ////////////////////////////////////////////////////////////////ВООООООООООООООООООООООООООООООООООООООООООООООООТТТТТТТТТТТТТ НАЧИНАЯ С ЭТОЙ Х***Ю И НИЖЕ НЕ РАБОТАЕТ ПОТОМУ ЧТО И НЕ ДОЛЖНО РАБОТАТЬ. СДЕЛАЛ ЧТОБЫ НА КАРТИНКЕ БЫЛО. НИЖЕ СТОИТ ОГРАНИЧЕНИЕ ГДЕ ЭТОТ НЕРАБОТАЮЩИЙ УЧАСТОК ЗАКАНЧИВАЕТСЯ (НА 87 СТРОК ВРОДЕ БЫ НИЖЕ) ЛИСТАЙ ВООБЩЕМ
            String mk200 = "131 131 131 131 -3 -3 -3 -3 -1 1 3 5 7 9 11 12 11 11 10 10 9 9 7 5 3 2 0 -2 -3 -3 -3 -3 -3 -3 -3 -3 -2 -1 0 2 3 4 5 8 12 16 20 24 28 32 32 33 33 32 32 31 31 32 34 36 37 39 41 43 43 43 43 44 44 44 45 46 47 48 49 50 51 52 52 53 53 54 54 55 56 57 59 60 61 63 64 65 66 68 69 70 72 73 74 76 78 80 81 83 85 86 88 89 91 93 94 96 97 98 99 99 100 101 102 102 102 102 101 101 101 101 100 99 98 97 96 95 94 93 91 90 89 87 86 85 84 84 83 83 82 82 81 81 81 81 80 80 80 80 79 78 76 75 74 73 72 71 70 69 69 68 67 66 65 64 63 62 61 60 59 57 56 54 53 52 50 49 48 48 47 46 45 45 44 44 44 44 43 43 43 41 40 38 37 36 34 33 31 29 27 25 23 21 20 21 23 25 27 29 31 31 26 21 15 10 5 0 -3 0 4 8 12 16 20 24 20 16 12 8 4 0 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3  ";
            w = mk200.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                mk200a[i] = Convert.ToInt16(w[i]);
                //panda_average = panda_average + panda[i];
                //nz_g655_3[i] = nz_g655_3[i] - nz_g655_3_average;
                //a9 += Math.Abs(nz_g655_3[i]);
                //nz_g655_2[i] = nz_g655_2[i] * aaaaa / a8;

            }
            for (int i = 0; i < 250; ++i)
            {
                chart14.Series[0].Points.AddXY(i, mk200a[i]);
            }
            String DSF2 = "30 30 33 36 38 41 43 44 45 46 46 47 48 48 48 49 49 49 50 50 50 51 52 52 53 53 54 55 55 56 56 57 58 60 62 63 65 66 67 67 68 69 70 71 72 74 75 76 77 78 79 80 81 81 82 82 83 83 84 84 85 86 86 87 87 88 88 89 89 89 89 90 90 90 90 89 89 88 88 88 87 87 86 86 86 85 85 84 84 83 82 82 81 81 80 79 79 78 78 77 77 77 76 76 76 76 76 75 75 75 75 76 76 76 77 77 77 78 78 79 79 79 79 78 78 78 78 78 79 79 79 79 78 78 77 77 77 76 76 76 75 75 74 74 73 73 72 72 72 72 72 72 72 72 71 71 71 71 71 71 71 71 71 71 71 70 70 70 70 71 71 72 72 72 72 71 71 71 71 71 71 71 71 70 70 69 68 68 67 67 66 66 66 66 66 65 65 64 64 63 62 62 61 60 59 58 58 57 56 56 56 56 56 56 56 54 52 51 49 48 48 48 48 48 48 46 44 42 40 39 38 38 37 37 36 35 35 34 33 33 31 30 28 27 25 24 24 24 23 23 ";
            w = DSF2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                mk200a[i] = Convert.ToInt16(w[i]);

            }
            for (int i = 0; i < 250; ++i)
            {
                chart15.Series[0].Points.AddXY(i, mk200a[i]);
            }
            panda_average = panda_average / 250;

            String erbc = "44 44 44 44 43 43 43 43 44 45 45 46 47 48 49 49 50 50 51 51 51 52 53 54 55 57 58 59 60 61 62 63 64 65 66 67 68 69 70 72 73 74 75 76 78 79 81 82 83 85 86 88 90 92 94 96 98 99 100 101 102 103 104 105 105 106 106 106 107 107 106 105 104 104 103 102 101 99 98 96 94 93 91 90 88 87 85 84 83 81 80 79 78 77 77 76 75 74 73 72 71 70 69 68 67 67 66 66 65 65 64 64 64 63 63 63 63 62 62 62 62 62 61 61 61 61 61 60 60 59 58 58 58 58 57 57 56 56 55 55 55 56 56 56 56 57 57 58 60 61 62 64 65 67 68 69 70 70 71 72 72 73 73 73 74 74 74 74 73 72 71 71 70 69 67 65 63 61 59 57 55 54 54 53 53 52 52 51 51 51 50 50 49 49 47 44 41 38 36 33 30 27 24 21 18 15 12 9 7 7 6 6 5 5 4 4 3 2 1 0 -1 -2 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -1 3 7 10 14 18 22 22 21 20 19 18 17 ";
            w = erbc.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                mk200a[i] = Convert.ToInt16(w[i]);

            }
            for (int i = 0; i < 250; ++i)
            {
                chart16.Series[0].Points.AddXY(i, mk200a[i]);
            }

            String erbg = "23 23 17 12 6 0 -3 -3 -3 -3 -3 -3 -3 -3 0 5 9 14 19 23 18 14 9 4 0 -3 -3 -3 -3 -3 -3 -3 0 4 8 12 16 20 24 25 24 23 22 21 19 17 15 12 10 8 6 4 3 1 0 -2 -3 1 7 13 19 25 31 37 39 40 40 41 41 42 41 41 40 39 39 38 39 39 40 41 41 42 44 46 48 50 52 54 56 56 56 56 56 56 56 57 59 61 63 65 66 68 69 69 70 71 72 73 74 75 76 77 78 79 80 82 83 85 86 88 89 91 93 94 96 98 99 99 100 101 101 102 101 101 100 99 98 98 97 95 93 91 89 87 86 84 82 81 79 77 76 76 75 75 75 74 74 73 72 71 70 69 68 67 65 64 63 61 60 59 58 57 57 56 55 54 52 51 50 48 47 46 46 45 45 44 44 43 41 37 32 28 23 19 16 18 21 24 26 29 30 32 31 30 29 28 25 21 17 12 8 4 0 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -3 -2 0 2 3 5 7 5 4 2 1 0 -2 -3 -3 -3 -3 -3 -3 -3 1 7 13 19 25 ";
            w = erbg.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                mk200a[i] = Convert.ToInt16(w[i]);

            }
            for (int i = 0; i < 250; ++i)
            {
                chart17.Series[0].Points.AddXY(i, mk200a[i]);
            }

            String lbl = "44 44 45 46 47 48 48 48 48 48 48 49 49 50 50 51 52 52 53 53 53 54 55 55 55 56 56 56 56 55 55 55 55 56 56 57 57 58 59 59 60 61 61 61 62 62 62 62 63 63 63 64 64 64 65 65 65 65 66 66 66 67 67 68 68 69 69 69 70 70 70 71 71 71 71 72 72 72 73 73 73 74 75 76 76 77 77 78 79 79 79 80 81 81 82 82 83 83 84 84 84 85 86 86 87 87 88 88 88 89 89 90 90 90 91 91 92 92 92 93 93 94 94 94 94 94 95 95 95 95 95 96 96 96 96 96 97 97 97 97 97 98 98 98 98 98 98 98 98 98 98 98 98 98 98 98 97 97 97 97 97 97 97 97 97 97 96 96 96 96 95 95 94 94 94 94 94 93 93 93 92 92 92 91 91 91 91 90 90 90 89 89 89 88 88 87 87 86 86 85 84 84 84 83 83 83 82 82 81 81 80 80 79 79 78 78 77 77 76 76 75 75 75 74 74 74 73 73 73 72 72 72 71 71 70 70 70 70 69 69 69 69 68 68 67 67 67 67 67 67 ";
            w = lbl.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                mk200a[i] = Convert.ToInt16(w[i]);

            }
            for (int i = 0; i < 250; ++i)
            {
                chart18.Series[0].Points.AddXY(i, mk200a[i]);
            }
            String leaf = "74 74 74 75 75 76 77 77 78 78 78 79 80 80 81 81 82 82 83 83 83 84 85 85 86 86 87 87 87 88 88 88 88 89 89 90 90 90 90 91 91 91 91 91 92 92 92 92 92 93 93 93 93 93 93 93 93 93 93 93 93 93 93 93 92 92 92 92 92 92 92 92 92 91 91 91 90 90 90 90 89 89 89 89 88 88 87 87 87 86 85 85 85 85 84 84 83 83 83 83 82 82 82 82 81 81 80 80 80 80 79 79 79 79 79 79 79 79 79 78 77 77 77 77 77 77 77 77 77 77 77 77 77 77 77 77 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 78 77 77 77 77 77 77 77 76 76 76 76 76 75 75 75 75 75 74 74 74 74 74 73 73 73 73 73 73 73 73 73 73 72 72 72 72 72 71 71 71 71 71 71 71 71 71 70 70 69 69 69 70 70 71 71 71 71 71 70 70 70 70 70 71 71 71 71 71 71 71 71 71 71 71 71 71 71 71 70 70 ";
            w = leaf.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                mk200a[i] = Convert.ToInt16(w[i]);

            }
            for (int i = 0; i < 250; ++i)
            {
                chart19.Series[0].Points.AddXY(i, mk200a[i]);
            }
            String panda_2 = "15 15 17 18 20 22 23 25 26 28 29 31 33 34 34 35 36 36 37 38 38 39 40 40 41 41 41 41 41 41 41 42 42 42 42 42 42 43 44 45 46 48 49 50 51 52 54 55 56 57 57 58 58 59 59 60 61 61 62 62 63 64 65 66 67 68 70 71 72 73 74 75 76 77 78 79 80 81 83 84 85 86 87 88 88 89 89 90 90 91 91 92 92 93 93 94 94 94 94 93 93 93 93 93 93 92 92 92 91 91 90 89 88 88 87 86 85 85 84 83 82 81 80 79 78 76 75 74 73 72 71 70 69 68 68 67 66 65 64 63 62 62 61 60 59 58 58 57 56 55 54 53 53 52 51 50 49 49 48 47 46 46 45 45 44 43 43 42 42 42 41 41 41 41 41 41 41 40 40 40 39 38 38 37 36 35 34 33 33 32 31 31 32 32 33 33 33 34 34 35 35 36 36 36 36 36 35 35 35 35 35 35 34 34 34 35 35 36 36 37 37 38 39 39 40 40 41 40 39 37 36 35 34 33 32 30 29 28 27 27 26 26 26 26 26 25 25 25 25 24 ";
            w = panda_2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 250; ++i)
            {
                mk200a[i] = Convert.ToInt16(w[i]);

            }
            for (int i = 0; i < 250; ++i)
            {
                chart20.Series[0].Points.AddXY(i, mk200a[i]);
            }
            String ultra = "32 32 34 37 39 41 42 41 41 40 40 40 41 41 42 42 42 43 43 44 43 43 42 42 41 40 39 40 41 42 42 43 41 39 37 35 34 36 38 41 43 46 48 49 49 50 51 52 52 53 53 54 54 54 54 54 54 54 53 53 52 52 52 53 53 54 54 54 55 55 55 56 56 57 57 58 58 59 59 59 59 60 60 60 60 61 61 61 62 63 64 65 66 67 67 68 68 69 69 70 70 71 71 71 71 72 72 72 72 73 73 74 74 75 75 76 76 77 78 79 80 81 82 82 82 82 83 83 83 83 83 84 85 85 86 86 87 87 88 88 88 88 88 88 88 89 89 90 90 90 90 91 92 92 92 92 92 92 92 92 92 93 93 93 93 93 93 93 93 93 93 93 93 93 93 93 93 92 92 92 92 92 91 91 91 91 91 91 91 91 91 91 90 90 90 90 90 89 89 89 89 89 88 88 88 87 87 86 86 85 85 85 86 86 86 85 85 84 84 83 82 82 81 81 80 80 80 80 79 79 79 79 79 78 78 78 78 78 77 77 76 76 75 75 74 73 73 73 72 72 ";
            w = ultra.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            /////todo///////////////////////////////////////////////////////////////////ВОООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООООТ ПО ЭТУ Х***Ю 
            for (int i = 0; i < 250; ++i)
            {
                mk200a[i] = Convert.ToInt16(w[i]);

            }
            for (int i = 0; i < 250; ++i)
            {
                chart21.Series[0].Points.AddXY(i, mk200a[i]);
            }

            for (int i = 0; i < 250; ++i)
            {
                panda[i] = panda[i] - panda_average;
                a10 += Math.Abs(panda[i]);
                nz_g655_3[i] = nz_g655_3[i] * aaaaa / a9;
                chart2.Series[0].Points.AddXY(i, dsf[i]);
                chart3.Series[0].Points.AddXY(i, dsf_bolshoy[i]);
                chart4.Series[0].Points.AddXY(i, erb[i]);
                chart5.Series[0].Points.AddXY(i, erb2[i]);
                chart6.Series[0].Points.AddXY(i, g652[i]);
                chart7.Series[0].Points.AddXY(i, g652_1[i]);
                chart8.Series[0].Points.AddXY(i, g652_2[i]);
                chart9.Series[0].Points.AddXY(i, nz_655_1[i]);
                chart10.Series[0].Points.AddXY(i, nz_g655_2[i]);
                chart11.Series[0].Points.AddXY(i, nz_g655_3[i]);
                ///chart12.Series[0].Points.AddXY(i, panda[i]);
            }
            a10 /= 250;
            for (int i = 0; i < 250; ++i)
            {
                panda[i] = panda[i] * aaaaa / a10;
                chart12.Series[0].Points.AddXY(i, panda[i]);
            }
            textBox2.Text = "dsf";
            //textBox14.Text = "erb";
            textBox15.Text = "erb";
            textBox16.Text = "erb2";
            textBox17.Text = "g652";
            textBox18.Text = "g652_1";
            textBox19.Text = "g652_2";
            textBox20.Text = "nz_g655_1";
            textBox21.Text = "nz_g655_2";
            textBox22.Text = "nz_g655_3";
            textBox23.Text = "panda";
             */
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart15_Click(object sender, EventArgs e)
        {

        }


    }
}
