using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DataAccessLayer.Bills;
using CinemaMagic.Models.DTOs;
using CinemaMagic.Models.DTOs.ProductManagement;
using CinemaMagic.Views.Statistics;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;


namespace CinemaMagic.ViewModels.StatisticsVM
{
    public class StatisticsViewModel : MainBaseViewModel
    {

        #region properties

        private string incomeText;
        public string IncomeText { get => incomeText; set { incomeText = value; OnPropertyChanged(nameof(IncomeText)); } }

        private string outcomeText;
        public string OutcomeText { get => outcomeText; set { outcomeText = value; OnPropertyChanged(nameof(OutcomeText)); } }

        // overall

        private ComboBoxItem _SelectedIncomePeriod;
        public ComboBoxItem SelectedIncomePeriod
        {
            get { return _SelectedIncomePeriod; }
            set { _SelectedIncomePeriod = value; OnPropertyChanged(nameof(SelectedIncomePeriod)); }
        }

        private string _SelectedIncomeTime;
        public string SelectedIncomeTime
        {
            get { return _SelectedIncomeTime; }
            set { _SelectedIncomeTime = value; OnPropertyChanged(nameof(SelectedIncomeTime)); }
        }

        // customer

        private ObservableCollection<CustomerStatisticsDTO> customerList = new();
        public ObservableCollection<CustomerStatisticsDTO> CustomerList
        {
            get => customerList;
            set { customerList = value; OnPropertyChanged(nameof(CustomerList)); }
        }

        private ComboBoxItem _SelectedCustomerIncomePeriod;
        public ComboBoxItem SelectedCustomerIncomePeriod
        {
            get { return _SelectedCustomerIncomePeriod; }
            set { _SelectedCustomerIncomePeriod = value; OnPropertyChanged(nameof(SelectedCustomerIncomePeriod)); }
        }

        private string _SelectedCustomerIncomeTime;
        public string SelectedCustomerIncomeTime
        {
            get { return _SelectedCustomerIncomeTime; }
            set { _SelectedCustomerIncomeTime = value; OnPropertyChanged(nameof(SelectedCustomerIncomeTime)); }
        }

        // movie

        private ObservableCollection<MovieStatisticsDTO> movieList = new();
        public ObservableCollection<MovieStatisticsDTO> MovieList
        {
            get => movieList;
            set { movieList = value; OnPropertyChanged(nameof(MovieList)); }
        }

        private ComboBoxItem _SelectedMovieIncomePeriod;
        public ComboBoxItem SelectedMovieIncomePeriod
        {
            get { return _SelectedMovieIncomePeriod; }
            set { _SelectedMovieIncomePeriod = value; OnPropertyChanged(nameof(SelectedMovieIncomePeriod)); }
        }

        private string _SelectedMovieIncomeTime;
        public string SelectedMovieIncomeTime
        {
            get { return _SelectedMovieIncomeTime; }
            set { _SelectedMovieIncomeTime = value; OnPropertyChanged(nameof(SelectedMovieIncomeTime)); }
        }


        // product

        private ObservableCollection<ProductStatisticsDTO> productList = new();
        public ObservableCollection<ProductStatisticsDTO> ProductList
        {
            get => productList;
            set { productList = value; OnPropertyChanged(nameof(ProductList)); }
        }

        private ComboBoxItem _SelectedProductIncomePeriod;
        public ComboBoxItem SelectedProductIncomePeriod
        {
            get { return _SelectedProductIncomePeriod; }
            set { _SelectedProductIncomePeriod = value; OnPropertyChanged(nameof(SelectedProductIncomePeriod)); }
        }

        private string _SelectedProductIncomeTime;
        public string SelectedProductIncomeTime
        {
            get { return _SelectedProductIncomeTime; }
            set { _SelectedProductIncomeTime = value; OnPropertyChanged(nameof(SelectedProductIncomeTime)); }
        }


        // current view

        private object currentStatisticsView;
        public object CurrentStatisticsView
        {
            get { return currentStatisticsView; }
            set { currentStatisticsView = value; OnPropertyChanged(nameof(CurrentStatisticsView)); }
        }


        #endregion

        #region livecharts

        private ISeries[] _IOSeries;
        public ISeries[] IOSeries { get { return _IOSeries; } set { _IOSeries = value; OnPropertyChanged(nameof(IOSeries)); } }

        private ISeries[] _OPSeries;
        public ISeries[] OPSeries { get { return _OPSeries; } set { _OPSeries = value; OnPropertyChanged(nameof(OPSeries)); } }

        private ISeries[] _IPSeries;
        public ISeries[] IPSeries { get { return _IPSeries; } set { _IPSeries = value; OnPropertyChanged(nameof(IPSeries)); } }

        public Axis[] IOXAxes { get; set; } =
        {
            new Axis
            {

                Labels = new string[] {""}
            }
        };

        public Axis[] CXAxes { get; set; } = new Axis[]
        {
            new TimeSpanAxis(TimeSpan.FromHours(1), date => date.ToString("%h") + "h")
        };

        public Axis[] CYAxes { get; set; } = {
            new Axis
            {
                Name="Number of guests"
            }
        };
        private ISeries[] _CLSeries;
        public ISeries[] CLSeries { get { return _CLSeries; } set { _CLSeries = value; OnPropertyChanged(nameof(CLSeries)); } }

        #endregion

        #region Commands

        // overall

        //test
        public IAsyncCommand ChangeIncomePeriodCommand { get; private set; }
        private async Task ExecuteChangeIncomePeriodCM()
        {
            if (SelectedIncomePeriod == null) return;
            else
            {
                switch (SelectedIncomePeriod.Content.ToString())
                {
                    case "By year":
                        {
                            if (SelectedIncomeTime != null)
                            {
                                await LoadByYear(SelectedIncomeTime);
                            }
                            return;
                        }
                    case "By month":
                        {
                            if (SelectedIncomeTime != null)
                            {
                                await LoadByMonth(SelectedIncomeTime.Substring(6));
                            }
                            return;
                        }
                }
            }
        }
        private async Task LoadByYear(string year)
        {
            BillAddMovieDA billAddMovieDA = new BillAddMovieDA();
            BillAddProductDA billAddProductDA = new BillAddProductDA();
            BillImportProductDA billImportProductDA = new BillImportProductDA();
            MovieDA movieDA = new MovieDA();
            BillDA billDA = new BillDA();
            ErrorDA errorDA = new ErrorDA();
            StaffDA staffDA = new StaffDA();
            long errorCostByYear = 0;
            long addProductCostByYear = 0;
            long movieCostByYear = 0;
            long sum_income = 0;
            long product_income = 0;
            long salaryCostByYear = 0;
            long sum_outcome = 0;
            try
            {
                //errorCostByYear = errorDA.GetCostByYear(year);
                //addProductCostByYear = billAddProductDA.GetOutcomeByYear(year) + billImportProductDA.GetOutcomeByYear(year);
                //movieCostByYear = movieDA.GetCostByYear(year);
                //sum_income = billDA.GetIncomeByYear(year);
                //product_income = billDA.GetProductIncomeByYear(year);
                //salaryCostByYear = staffDA.GetSalaryByYear(year);
                //sum_outcome = addProductCostByYear + errorCostByYear + movieCostByYear + salaryCostByYear;

                Task<long> taskErrorCost = Task.Run(() => errorDA.GetCostByYear(year));
                Task<long> taskBillAddProduct = Task.Run(() => billAddProductDA.GetOutcomeByYear(year));
                Task<long> taskBillImportProduct = Task.Run(() => billImportProductDA.GetOutcomeByYear(year));
                Task<long> taskMovieCost = Task.Run(() => movieDA.GetCostByYear(year));
                Task<long> taskSumIncome = Task.Run(() => billDA.GetIncomeByYear(year));
                Task<long> taskProductIncome = Task.Run(() => billDA.GetProductIncomeByYear(year));
                Task<long> taskSalary = Task.Run(() => staffDA.GetSalaryByYear(year));

                await Task.WhenAll(taskErrorCost, taskBillAddProduct, taskBillImportProduct, taskMovieCost, taskSumIncome, taskProductIncome, taskSalary);
                errorCostByYear = taskErrorCost.Result;
                addProductCostByYear = taskBillAddProduct.Result + taskBillImportProduct.Result;
                movieCostByYear = taskMovieCost.Result;
                sum_income = taskSumIncome.Result;
                product_income = taskProductIncome.Result;
                salaryCostByYear = taskSalary.Result;
                sum_outcome = addProductCostByYear + errorCostByYear + movieCostByYear + salaryCostByYear;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IncomeText = sum_income.ToString("N0");
                OutcomeText = sum_outcome.ToString("N0");

                IOSeries = new ISeries[]
                {
                    new ColumnSeries<long>
                    {
                        Values = new List<long>{ sum_income},
                        Name = "Revenue"
                    },
                    new ColumnSeries<long>
                    {
                        Values = new List<long>{ sum_outcome },
                        Name = "Cost"
                    }
                };
                OPSeries = new ISeries[]
                {
                    new PieSeries<long>
                    {
                        Values = new long[] {errorCostByYear},
                        Name = "Incident"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {addProductCostByYear},
                        Name = "Enter food items"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {movieCostByYear},
                        Name = "Enter movies"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {salaryCostByYear},
                        Name = "Employee salary"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {movieCostByYear/20},
                        Name = "Voucher"
                    }
                };
                IPSeries = new ISeries[]
                {
                    new PieSeries<long>
                    {
                        Values = new long[] {sum_income-product_income},
                        Name = "Tickets"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {product_income},
                        Name = "Products"
                    }
                };
            }
        }

        private async Task LoadByMonth(string month)
        {
            BillAddMovieDA billAddMovieDA = new BillAddMovieDA();
            BillAddProductDA billAddProductDA = new BillAddProductDA();
            BillImportProductDA billImportProductDA = new BillImportProductDA();
            MovieDA movieDA = new MovieDA();
            BillDA billDA = new BillDA();
            ErrorDA errorDA = new ErrorDA();
            StaffDA staffDA = new StaffDA();
            long errorCostByM = 0;
            long addProductCostByM = 0;
            long movieCostByM = 0;
            long sum_income = 0;
            long product_income = 0;
            long salaryCostByM = 0;
            long sum_outcome = 0;
            try
            {
                //errorCostByYear = errorDA.GetCostByYear(year);
                //addProductCostByYear = billAddProductDA.GetOutcomeByYear(year) + billImportProductDA.GetOutcomeByYear(year);
                //movieCostByYear = movieDA.GetCostByYear(year);
                //sum_income = billDA.GetIncomeByYear(year);
                //product_income = billDA.GetProductIncomeByYear(year);
                //salaryCostByYear = staffDA.GetSalaryByYear(year);
                //sum_outcome = addProductCostByYear + errorCostByYear + movieCostByYear + salaryCostByYear;

                Task<long> taskErrorCost = Task.Run(() => errorDA.GetCostByMonth(month));
                Task<long> taskBillAddProduct = Task.Run(() => billAddProductDA.GetOutcomeByMonth(month));
                Task<long> taskBillImportProduct = Task.Run(() => billImportProductDA.GetOutcomeByMonth(month));
                Task<long> taskMovieCost = Task.Run(() => movieDA.GetCostByMonth(month));
                Task<long> taskSumIncome = Task.Run(() => billDA.GetIncomeByMonth(month));
                Task<long> taskProductIncome = Task.Run(() => billDA.GetProductIncomeByMonth(month));
                Task<long> taskSalary = Task.Run(() => staffDA.GetSalaryByMonth(month));

                await Task.WhenAll(taskErrorCost, taskBillAddProduct, taskBillImportProduct, taskMovieCost, taskSumIncome, taskProductIncome, taskSalary);
                errorCostByM = taskErrorCost.Result;
                addProductCostByM = taskBillAddProduct.Result + taskBillImportProduct.Result;
                movieCostByM = taskMovieCost.Result;
                sum_income = taskSumIncome.Result;
                product_income = taskProductIncome.Result;
                salaryCostByM = taskSalary.Result;
                sum_outcome = addProductCostByM + errorCostByM + movieCostByM + salaryCostByM;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IncomeText = sum_income.ToString("N0");
                OutcomeText = sum_outcome.ToString("N0");

                IOSeries = new ISeries[]
                {
                    new ColumnSeries<long>
                    {
                        Values = new List<long>{ sum_income},
                        Name = "Revenue"
                    },
                    new ColumnSeries<long>
                    {
                        Values = new List<long>{ sum_outcome },
                        Name = "Cost"
                    }
                };
                OPSeries = new ISeries[]
                {
                    new PieSeries<long>
                    {
                        Values = new long[] {errorCostByM},
                        Name = "Incident"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {addProductCostByM},
                        Name = "Enter food items"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {movieCostByM},
                        Name = "Enter movies"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {salaryCostByM},
                        Name = "Employee salary"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {movieCostByM/20},
                        Name = "Voucher"
                    }
                };
                IPSeries = new ISeries[]
                {
                    new PieSeries<long>
                    {
                        Values = new long[] {sum_income-product_income},
                        Name = "Tickets"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {product_income},
                        Name = "Products"
                    }
                };
            }
        }


        // customer

        public ICommand ChangeCustomerIncomePeriodCommand { get; set; }

        private void ExecuteChangeCustomerIncomePeriodCM(object obj)
        {
            if (SelectedCustomerIncomePeriod == null) return;
            else
            {
                switch (SelectedCustomerIncomePeriod.Content.ToString())
                {
                    case "By year":
                        {
                            if (SelectedCustomerIncomeTime != null)
                            {
                                LoadCustomerByYear(SelectedCustomerIncomeTime);
                            }
                            return;
                        }
                    case "By month":
                        {
                            if (SelectedCustomerIncomeTime != null)
                            {
                                LoadCustomerByMonth(SelectedCustomerIncomeTime.Substring(6));
                            }
                            return;
                        }
                }
            }
        }

        private void LoadCustomerByMonth(string month)
        {
            CustomerDA customerDA = new CustomerDA();
            List<CustomerStatisticsDTO> cusList = customerDA.GetTopCustomerByMonth(month);
            CustomerList = new ObservableCollection<CustomerStatisticsDTO>(cusList);
        }
        private void LoadCustomerByYear(string year)
        {
            CustomerDA customerDA = new CustomerDA();
            List<CustomerStatisticsDTO> cusList = customerDA.GetTopCustomerByYear(year);
            CustomerList = new ObservableCollection<CustomerStatisticsDTO>(cusList);
        }

        // Movie

        public ICommand ChangeMovieIncomePeriodCommand { get; set; }

        private void ExecuteChangeMovieIncomePeriodCM(object obj)
        {
            if (SelectedMovieIncomePeriod == null) return;
            else
            {
                switch (SelectedMovieIncomePeriod.Content.ToString())
                {
                    case "By year":
                        {
                            if (SelectedMovieIncomeTime != null)
                            {
                                LoadMovieByYear(SelectedMovieIncomeTime);
                            }
                            return;
                        }
                    case "By month":
                        {
                            if (SelectedMovieIncomeTime != null)
                            {
                                LoadMovieByMonth(SelectedMovieIncomeTime.Substring(6));
                            }
                            return;
                        }
                }
            }
        }

        private void LoadMovieByMonth(string month)
        {
            MovieDA movieDA = new MovieDA();
            List<MovieStatisticsDTO> movList = movieDA.GetTopMovieByMonth(month);
            MovieList = new ObservableCollection<MovieStatisticsDTO>(movList);
        }
        private void LoadMovieByYear(string year)
        {
            MovieDA movieDA = new();
            List<MovieStatisticsDTO> movList = movieDA.GetTopMovieByYear(year);
            MovieList = new ObservableCollection<MovieStatisticsDTO>(movList);
        }

        // product

        public ICommand ChangeProductIncomePeriodCommand { get; set; }

        private void ExecuteChangeProductIncomePeriodCM(object obj)
        {
            if (SelectedProductIncomePeriod == null) return;
            else
            {
                switch (SelectedProductIncomePeriod.Content.ToString())
                {
                    case "By year":
                        {
                            if (SelectedProductIncomeTime != null)
                            {
                                LoadProductByYear(SelectedProductIncomeTime);
                            }
                            return;
                        }
                    case "By month":
                        {
                            if (SelectedProductIncomeTime != null)
                            {
                                LoadProductByMonth(SelectedProductIncomeTime.Substring(6));
                            }
                            return;
                        }
                }
            }
        }

        private void LoadProductByMonth(string month)
        {
            ProductDA productDA = new();
            List<ProductStatisticsDTO> pList = productDA.GetTopProductByMonth(month);
            ProductList = new ObservableCollection<ProductStatisticsDTO>(pList);
        }
        private void LoadProductByYear(string year)
        {
            ProductDA productDA = new();
            List<ProductStatisticsDTO> pList = productDA.GetTopProductByYear(year);
            ProductList = new ObservableCollection<ProductStatisticsDTO>(pList);
        }



        // switch view
        public ICommand SwitchViewStatisticsCommand { get; set; }

        private void SwitchViewStatistics(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch (UserControlName)
            {
                case "Movie":
                    CurrentStatisticsView = new StatisticsMovie();
                    break;
                case "Overall":
                    CurrentStatisticsView = new StatisticsOverallView();
                    break;
                case "Product":
                    CurrentStatisticsView = new StatisticsProduct();
                    break;
                case "Customer":
                    List<TimeSpanPoint> timeSpanPoints = new List<TimeSpanPoint>();
                    BillDA billService = new BillDA();
                    try
                    {

                        List<Tuple<int, int>> tuples = billService.GetCustomerDistribution();
                        int iT = 0;
                        for (int h = 0; h <= 23; ++h)
                        {
                            if (iT >= tuples.Count)
                            {
                                timeSpanPoints.Add(new TimeSpanPoint(TimeSpan.FromHours(h), 0));
                            }
                            else
                            {
                                if (tuples[iT].Item1 == h)
                                {
                                    timeSpanPoints.Add(new TimeSpanPoint(TimeSpan.FromHours(h), tuples[iT].Item2));
                                    ++iT;
                                }
                                else
                                {
                                    timeSpanPoints.Add(new TimeSpanPoint(TimeSpan.FromHours(h), 0));
                                }
                            }
                        }


                    }
                    catch
                    {

                    }
                    finally
                    {
                        CLSeries = new ISeries[]
                        {
                            new LineSeries<TimeSpanPoint>
                            {
                                Values = timeSpanPoints
                            }
                        };
                    }


                    CurrentStatisticsView = new StatisticsCustomerView();
                    break;
            }
        }

        #endregion

        public StatisticsViewModel()
        {
            CurrentStatisticsView = new StatisticsOverallView();
            SwitchViewStatisticsCommand = new ViewModelCommand(SwitchViewStatistics);
            ChangeIncomePeriodCommand = new AsyncCommand(ExecuteChangeIncomePeriodCM);
            ChangeCustomerIncomePeriodCommand = new ViewModelCommand(ExecuteChangeCustomerIncomePeriodCM);
            ChangeMovieIncomePeriodCommand = new ViewModelCommand(ExecuteChangeMovieIncomePeriodCM);
            ChangeProductIncomePeriodCommand = new ViewModelCommand(ExecuteChangeProductIncomePeriodCM);
        }


    }
}
