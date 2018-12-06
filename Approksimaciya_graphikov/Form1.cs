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

        private Component _component;
        private Data _data = new Data();
        private List<Chart> _charts = new List<Chart>();
        private List<List<double>> _chartsCoordinates_Y = new List<List<double>>();
        private List<List<double>> _chartsCoordinates_X = new List<List<double>>();
        private double[] _koordinaty_graphika = new double[250];
        private int[,] _koordinaty_graphika_new = new int[250, 250];
        private int _temp = 0;
        private bool _firstStart = true;
        //Разрешения монитора
        private int _heightMonitor;
        private int _widhtMonitor;
        //Количество графиков, которое будет помещаться в одну строку монитора
        private int _nubmerOfGrapf;
        private List<Point> _tempLocationGrapf = new List<Point>();



        protected override void OnPaint(PaintEventArgs e)
        {
            //Перегруженная функция , выполняется каждый раз, когда обновляется форма примерно раз в секунду, поэтому снизу q==0, чтобы выполнилось 1 раз
            base.OnPaint(e);
            if (_firstStart)
            {
                createGrafics();
            }
            _firstStart = false;
        }

        private void createGrafics()
        {
            /// Код ниже срабатывает при запуске программы
            Deserialize();
            int graficsCount = (_data.data.Count);
            int shiftX = 0;
            int shiftY = 0;

            _nubmerOfGrapf = _widhtMonitor / 300;
            panel1.Height = 2 * 190 + 10;
            panel1.Width = _nubmerOfGrapf * 310;

            for (int i = 0; i < graficsCount; i++)
            {
                if (i % _nubmerOfGrapf == 0 && i != 0)
                {
                    shiftX += 190;
                    shiftY = 0;
                }
                //  Point startLocation = new Point(16 + shiftY, 388 + shiftX); // 16 388
                Point startLocation = new Point(shiftY, shiftX); // 16 388
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
                _charts.Add(myChart);
                myChart.Series.Add(new Series());
                // myChart.Series[0].Points.Clear();
                myChart.Series[0].Enabled = true;
                myChart.Enabled = true;
                panel1.Controls.Add(myChart);
                List<double> coordinatesY = new List<double>();
                List<double> coordinatesX = new List<double>();
                for (int j = 0; j < 250; j++)
                {
                    myChart.Series[0].Points.AddXY(_data.data[i].coordinatesX[j], _data.data[i].coordinatesY[j]);
                    coordinatesY.Add(_data.data[i].coordinatesY[j]);
                    coordinatesX.Add(_data.data[i].coordinatesX[j]);
                    // data экземплял класса, закруженного из файла при запуске -- функция Deserialize()
                }
                _chartsCoordinates_Y.Add(coordinatesY);
                _chartsCoordinates_X.Add(coordinatesX);
            }

            //Заполнение кооридант
            for (int i = 0; i < _charts.Count; i++)
            {
                _tempLocationGrapf.Add(_charts[i].Location);
            }

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
            textBox1.Clear();
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

        private void button4_Click(object sender, EventArgs e) // Аппроксимировать
        {

            chart1.Series[0].Points.Clear();
            Bitmap input = new Bitmap(pictureBox2.Image);
            bool isBlack = false;
            // получаем (свободный пиксель, чтобы определить цвет фона графика
            UInt32 pixel = (UInt32)(input.GetPixel(0, input.Height - 1).ToArgb());
            float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый  

            //Перелистывает скролл в начальное положение (необходимо, чтобы в панели графики располагались с начала координат, а не с середины)
            panel1.VerticalScroll.Value = panel1.VerticalScroll.Minimum;


            //если цвет свободного пикселя чёрный, то G=0, иначе G=255
            if (G == 0)
            {
                isBlack = true;
            }

            //Проверка: чёрный или нет? (см. методы ниже, рядом с поиском кореляции)
            if (isBlack)
            {
                _koordinaty_graphika = grapfBlack(input);
            }
            else _koordinaty_graphika = grapfWhite(input);

            ///// Тут начинается логика
            double stepY = 1;
            double max = 0;
            for (int i = 0; i < _koordinaty_graphika.Length; i++)
            {

                if (i > 10 && _koordinaty_graphika[i] > max)
                    max = _koordinaty_graphika[i];
            }

            if (!textBox4.Text.Equals(""))
            {


                stepY = Convert.ToDouble(textBox4.Text) / max;

            }


            double[] frequencyCoordinates = new double[250];

            for (int i = 0; i < input.Width; i++)
            {

                int stepX = 1;
                if (!textBox3.Text.Equals("") && !textBox2.Text.Equals(""))
                {
                    stepX = (Convert.ToInt32(textBox3.Text) - Convert.ToInt32(textBox2.Text)) / 250;
                }

                //koordinaty_graphika[i] = koordinaty_graphika[i] - koordinaty_graphika_average;
                _koordinaty_graphika[i] = _koordinaty_graphika[i] * stepY;
                chart1.Series[0].Points.AddXY(i * stepX, _koordinaty_graphika[i]);
                frequencyCoordinates[i] = i * stepX;

            }
            _component = new Component();
            _component.setCoordinates(frequencyCoordinates, _koordinaty_graphika); // Запоминаем график для дальнейшей сериализации

            List<double> correlationCoef_Y = new List<double>();
            List<double> correlationCoef_X = new List<double>();

            List<double> rPirsonCorel_Y = new List<double>();
            List<double> rPirsonCorel_X = new List<double>();


            for (int i = 0; i < _charts.Count; i++)
            {
                rPirsonCorel_Y.Add(rPirson(_koordinaty_graphika, _chartsCoordinates_Y[i].ToArray()));
                rPirsonCorel_X.Add(rPirson(frequencyCoordinates, _chartsCoordinates_X[i].ToArray()));
            }
            List<double> correlationCoefAverage = new List<double>();

            for (int i = 0; i < rPirsonCorel_Y.Count; i++)
            {
                correlationCoefAverage.Add(rPirsonCorel_Y[i]);
            }


            //   for (int i = 0; i < _charts.Count; i++)
            //      {
            //         correlationCoef_Y.Add(findCorrelation(_koordinaty_graphika, _chartsCoordinates_Y[i].ToArray()));
            //         correlationCoef_X.Add(findCorrelation(frequencyCoordinates, _chartsCoordinates_X[i].ToArray()));
            //     }
            //     List<double> correlationCoefAverage = new List<double>();
            //     
            ///      for (int i = 0; i < correlationCoef_X.Count; i++)
            //      {
            //          correlationCoefAverage.Add(correlationCoef_Y[i]);
            //      }
            //Метод который красит графики и располагает их в порядке уменьшения кореляции (см. ниже)

            coloringGraphs(correlationCoefAverage);


        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }



        private double[] grapfBlack(Bitmap input)
        {
            double[] koordinaty_graphika = new double[250];
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
                    if ((G == 51) && (_temp == 0) && (j == 0) || (G == 51) && (_temp != 0) && (j != 0))
                    {
                        _temp = 1;
                        koordinaty_graphika[j] = 140 - i;
                        //chart1.Series[0].Points.AddXY(j, koordinaty_graphika[j]);
                        textBox1.Text = textBox1.Text + (koordinaty_graphika[j]).ToString() + ' ';
                        break;
                    }
                }
            }

            return koordinaty_graphika;
        }

        private double[] grapfWhite(Bitmap input, params int[] frequency)
        {
            int step = 1;
            if (frequency.Length != 0)
            {
                step = (frequency[1] - frequency[0]) / 250;
            }
            double[] koordinaty_graphika_Y = new double[250];
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

                    if ((G == 0) && (_temp == 0) && (j % 10 != 0))
                    // if ((G == 0) && (temp == 0))
                    {
                        koordinaty_graphika_Y[j] = input.Height - i;
                        textBox1.Text = textBox1.Text + (koordinaty_graphika_Y[j]).ToString() + ' ';
                        //  koordinaty_graphika[j] = i;
                        //chart1.Series[0].Points.AddXY(j, koordinaty_graphika[j]);
                        break;
                    }

                }

                if ((j % 10 == 1) && (j > 10))
                {
                    koordinaty_graphika_Y[j - 1] = (koordinaty_graphika_Y[j] + koordinaty_graphika_Y[j - 2]) / 2;
                }
            }
            return koordinaty_graphika_Y;
        }

        private double rPirson(double[] x, double[] y)
        {
            double xAverage = 0;
            for (int i = 0; i < x.Length; i++)
            {
                xAverage += x[i];
            }
            xAverage = xAverage / x.Length;

            double yAverage = 0;
            for (int i = 0; i < y.Length; i++)
            {
                yAverage += y[i];
            }
            yAverage = yAverage / y.Length;

            double[] xNumerator = new double[x.Length];
            double sumXNumerator = 0;
            for (int i = 0; i < x.Length; i++)
            {
                xNumerator[i] = x[i] - xAverage;
                sumXNumerator += xNumerator[i];
            }

            double[] yNumerator = new double[y.Length];
            double sumYNumerator = 0;
            for (int i = 0; i < y.Length; i++)
            {
                yNumerator[i] = y[i] - yAverage;
                sumYNumerator += yNumerator[i];
            }

            double[] squadXNumerator = new double[x.Length];
            double sumSquadXNumerator = 0;

            for (int i = 0; i < x.Length; i++)
            {
                squadXNumerator[i] = Math.Pow(xNumerator[i], 2);
                sumSquadXNumerator += squadXNumerator[i];
            }

            double[] squadYNumerator = new double[y.Length];
            double sumSquadYNumerator = 0;

            for (int i = 0; i < y.Length; i++)
            {
                squadYNumerator[i] = Math.Pow(yNumerator[i], 2);
                sumSquadYNumerator += squadYNumerator[i];
            }

            double[] xyNum = new double[x.Length];
            double sumXYNum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                xyNum[i] = (x[i] - xAverage) * (y[i] - yAverage);
                sumXYNum += xyNum[i];
            }

            double sigmaX = Math.Sqrt(sumSquadXNumerator / (x.Length - 1));
            double sigmaY = Math.Sqrt(sumSquadYNumerator / (y.Length - 1));

            double rPirson = sumXYNum / (sigmaX * sigmaY * (x.Length - 1));
            return rPirson;
        }


        private double findCorrelation(double[] x, double[] y)
        {
            double xAverage = 0;
            for (int i = 0; i < x.Length; i++)
            {
                xAverage += x[i];
            }
            xAverage = xAverage / x.Length;
            double yAverage = 0;
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

        private void coloringGraphs(List<double> correlationCoefY)
        {

            int[] indexDescending = new int[correlationCoefY.Count];   //массив индексов в порядке убывания коэффициента корреляции
            double[] tempCorrelation = new double[correlationCoefY.Count]; //временный массив кореляций с которым работаем
            bool[] checkBoolean = new bool[correlationCoefY.Count]; //массив для проверки: Был ли уже записан номер графика в нужный индекс?

            //Запись массива коэффициентов кореляции во временный массив
            for (int i = 0; i < correlationCoefY.Count; i++)
            {
                tempCorrelation[i] = correlationCoefY[i];
            }
            Array.Sort(tempCorrelation);
            Array.Reverse(tempCorrelation);


            //Ищет индексы графиков с максимальной кореляцией в порядке убывания
            for (int i = 0; i < tempCorrelation.Length; i++)
            {
                for (int j = 0; j < correlationCoefY.Count; j++)
                {
                    if (tempCorrelation[i] == correlationCoefY[j] && checkBoolean[i] == false)
                    {
                        indexDescending[i] = j;
                        checkBoolean[i] = true;
                    }
                }
            }

            //Алгоритм покраски графиков

            for (int i = 0; i < indexDescending.Length; i++)
            {
                if (i == 0)
                {
                    _charts[indexDescending[i]].BackColor = Color.Green;
                }
                else if (i > 0 && i < 3)
                {
                    _charts[indexDescending[i]].BackColor = Color.Orange;
                }
                else _charts[indexDescending[i]].BackColor = Color.Red;
            }

            //Меняет местами графики в порядке убывания кореляции
            for (int i = 0; i < _charts.Count; i++)
            {
                for (int j = 0; j < _charts.Count; j++)
                {
                    if (i == indexDescending[j])
                    {
                        _charts[i].Location = _tempLocationGrapf[j];
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            this._data.addComponent(_component); // Кнопка добавить

        }

        private void button6_Click(object sender, EventArgs e) // Кнопка серилизовать
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("./data.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this._data);
            }
            this._firstStart = true;
        }

        private void Deserialize()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("./data.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    _data = (Data)formatter.Deserialize(fs);
                }
                catch (Exception e)
                {
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            _heightMonitor = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height;
            _widhtMonitor = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width;



        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart15_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click_1(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup_1(object sender, PopupEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }



    }
}
