using FinalProject.ATMPackage;
using FinalProject.CardPackage;
using FinalProject.CompanyPackage;
using FinalProject.FilterPackage;
using FinalProject.ProgramPackage;
using FinalProject.TransactionPackage;
using FinalProject.TransactionPackages;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        private Account checkingAccount;
        private Account savingsAccount;
        private Account transferAccount;

        private CardIF activeCard;
        private CardIF transferCard;

        private Data atmData;
        private Company company;
        private ReadAndWrite readAndWrite;

        private Label titleLabel;
        private Label balanceLabel;
        private Label savingsLabel;
        private Label transferLabel;
        private Label atmCashLabel;
        private Label maxCashLabel;
        private Label cardStatusLabel;
        private Label physicalInfoLabel;

        private TextBox pinBox;
        private TextBox cardNumberBox;
        private TextBox cv2Box;
        private TextBox expirationBox;
        private TextBox amountBox;

        private RadioButton physicalRadio;
        private RadioButton virtualRadio;

        private Button createCardButton;
        private Button depositButton;
        private Button withdrawButton;
        private Button transferButton;
        private Button refreshButton;
        private Button clearLogButton;

        private ListBox logBox;

        public Form1()
        {
            InitializeComponent();
            SetupBackend();
            BuildFrontend();
            UpdateCardMenu();
            RefreshDisplay();
        }

        private void SetupBackend()
        {
            checkingAccount = new Account();
            checkingAccount.SetAccountId("CHECKING-1001");
            checkingAccount.SetBalance(750.00m);

            savingsAccount = new Account();
            savingsAccount.SetAccountId("SAVINGS-1002");
            savingsAccount.SetBalance(1200.00m);

            transferAccount = new Account();
            transferAccount.SetAccountId("TRANSFER-2001");
            transferAccount.SetBalance(300.00m);

            atmData = new Data();
            atmData.SetCash(2500.00m);
            atmData.SetMaximumCashOut(500.00m);

            company = new Company();
            readAndWrite = new ReadAndWrite();

            readAndWrite.SaveAccount(checkingAccount);
            readAndWrite.SaveAccount(savingsAccount);
            readAndWrite.SaveAccount(transferAccount);

            transferCard = new Virtual();
            transferCard.SetAccount(transferAccount);
            transferCard.SetPIN("9999");
            transferCard.SetCardNumber("9999-8888-7777-6666");
            transferCard.SetCV2("555");
            transferCard.SetExpirationDate("01/29");
            transferCard.SetValid();
        }

        private void BuildFrontend()
        {
            this.Text = "SmartATM";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(235, 240, 245);

            titleLabel = new Label();
            titleLabel.Text = "SmartATM Banking System";
            titleLabel.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(25, 55, 95);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(30, 20);
            this.Controls.Add(titleLabel);

            BuildCardPanel();
            BuildTransactionPanel();
            BuildStatusPanel();
            BuildLogPanel();
        }

        private void BuildCardPanel()
        {
            GroupBox cardGroup = new GroupBox();
            cardGroup.Text = "Card Program";
            cardGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            cardGroup.Location = new Point(30, 85);
            cardGroup.Size = new Size(430, 315);
            this.Controls.Add(cardGroup);

            physicalRadio = new RadioButton();
            physicalRadio.Text = "Physical Card";
            physicalRadio.Location = new Point(25, 35);
            physicalRadio.Size = new Size(160, 25);
            physicalRadio.Checked = true;
            physicalRadio.CheckedChanged += CardTypeChanged;
            cardGroup.Controls.Add(physicalRadio);

            virtualRadio = new RadioButton();
            virtualRadio.Text = "Virtual Card";
            virtualRadio.Location = new Point(210, 35);
            virtualRadio.Size = new Size(160, 25);
            virtualRadio.CheckedChanged += CardTypeChanged;
            cardGroup.Controls.Add(virtualRadio);

            physicalInfoLabel = new Label();
            physicalInfoLabel.Location = new Point(25, 65);
            physicalInfoLabel.Size = new Size(360, 35);
            physicalInfoLabel.ForeColor = Color.FromArgb(25, 55, 95);
            physicalInfoLabel.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            cardGroup.Controls.Add(physicalInfoLabel);

            Label pinLabel = new Label();
            pinLabel.Text = "PIN:";
            pinLabel.Location = new Point(25, 110);
            pinLabel.Size = new Size(130, 25);
            cardGroup.Controls.Add(pinLabel);

            pinBox = new TextBox();
            pinBox.Location = new Point(170, 110);
            pinBox.Size = new Size(210, 25);
            pinBox.Text = "1234";
            cardGroup.Controls.Add(pinBox);

            Label numberLabel = new Label();
            numberLabel.Text = "Card Number:";
            numberLabel.Location = new Point(25, 150);
            numberLabel.Size = new Size(130, 25);
            cardGroup.Controls.Add(numberLabel);

            cardNumberBox = new TextBox();
            cardNumberBox.Location = new Point(170, 150);
            cardNumberBox.Size = new Size(210, 25);
            cardNumberBox.Text = "1234-5678-9999-0000";
            cardGroup.Controls.Add(cardNumberBox);

            Label cv2Label = new Label();
            cv2Label.Text = "CV2:";
            cv2Label.Location = new Point(25, 190);
            cv2Label.Size = new Size(130, 25);
            cardGroup.Controls.Add(cv2Label);

            cv2Box = new TextBox();
            cv2Box.Location = new Point(170, 190);
            cv2Box.Size = new Size(210, 25);
            cv2Box.Text = "123";
            cardGroup.Controls.Add(cv2Box);

            Label expLabel = new Label();
            expLabel.Text = "Expiration:";
            expLabel.Location = new Point(25, 230);
            expLabel.Size = new Size(130, 25);
            cardGroup.Controls.Add(expLabel);

            expirationBox = new TextBox();
            expirationBox.Location = new Point(170, 230);
            expirationBox.Size = new Size(210, 25);
            expirationBox.Text = "12/28";
            cardGroup.Controls.Add(expirationBox);

            createCardButton = new Button();
            createCardButton.Text = "Create / Validate Card";
            createCardButton.Location = new Point(110, 270);
            createCardButton.Size = new Size(210, 35);
            createCardButton.BackColor = Color.FromArgb(40, 100, 180);
            createCardButton.ForeColor = Color.White;
            createCardButton.FlatStyle = FlatStyle.Flat;
            createCardButton.Click += CreateCardButton_Click;
            cardGroup.Controls.Add(createCardButton);
        }

        private void BuildTransactionPanel()
        {
            GroupBox transactionGroup = new GroupBox();
            transactionGroup.Text = "Transaction Controls";
            transactionGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            transactionGroup.Location = new Point(500, 85);
            transactionGroup.Size = new Size(430, 315);
            this.Controls.Add(transactionGroup);

            Label amountLabel = new Label();
            amountLabel.Text = "Amount:";
            amountLabel.Location = new Point(25, 45);
            amountLabel.Size = new Size(120, 25);
            transactionGroup.Controls.Add(amountLabel);

            amountBox = new TextBox();
            amountBox.Location = new Point(150, 45);
            amountBox.Size = new Size(220, 25);
            amountBox.Text = "50";
            transactionGroup.Controls.Add(amountBox);

            depositButton = new Button();
            depositButton.Text = "Deposit";
            depositButton.Location = new Point(30, 100);
            depositButton.Size = new Size(115, 50);
            depositButton.BackColor = Color.FromArgb(45, 145, 75);
            depositButton.ForeColor = Color.White;
            depositButton.FlatStyle = FlatStyle.Flat;
            depositButton.Click += DepositButton_Click;
            transactionGroup.Controls.Add(depositButton);

            withdrawButton = new Button();
            withdrawButton.Text = "Withdraw";
            withdrawButton.Location = new Point(160, 100);
            withdrawButton.Size = new Size(115, 50);
            withdrawButton.BackColor = Color.FromArgb(200, 120, 40);
            withdrawButton.ForeColor = Color.White;
            withdrawButton.FlatStyle = FlatStyle.Flat;
            withdrawButton.Click += WithdrawButton_Click;
            transactionGroup.Controls.Add(withdrawButton);

            transferButton = new Button();
            transferButton.Text = "Transfer";
            transferButton.Location = new Point(290, 100);
            transferButton.Size = new Size(115, 50);
            transferButton.BackColor = Color.FromArgb(90, 90, 190);
            transferButton.ForeColor = Color.White;
            transferButton.FlatStyle = FlatStyle.Flat;
            transferButton.Click += TransferButton_Click;
            transactionGroup.Controls.Add(transferButton);

            refreshButton = new Button();
            refreshButton.Text = "Refresh Status";
            refreshButton.Location = new Point(75, 185);
            refreshButton.Size = new Size(130, 40);
            refreshButton.Click += RefreshButton_Click;
            transactionGroup.Controls.Add(refreshButton);

            clearLogButton = new Button();
            clearLogButton.Text = "Clear Log";
            clearLogButton.Location = new Point(225, 185);
            clearLogButton.Size = new Size(130, 40);
            clearLogButton.Click += ClearLogButton_Click;
            transactionGroup.Controls.Add(clearLogButton);
        }

        private void BuildStatusPanel()
        {
            GroupBox statusGroup = new GroupBox();
            statusGroup.Text = "Backend Status";
            statusGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            statusGroup.Location = new Point(30, 425);
            statusGroup.Size = new Size(430, 220);
            this.Controls.Add(statusGroup);

            balanceLabel = new Label();
            balanceLabel.Location = new Point(25, 35);
            balanceLabel.Size = new Size(370, 25);
            balanceLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            statusGroup.Controls.Add(balanceLabel);

            savingsLabel = new Label();
            savingsLabel.Location = new Point(25, 70);
            savingsLabel.Size = new Size(370, 25);
            savingsLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            statusGroup.Controls.Add(savingsLabel);

            transferLabel = new Label();
            transferLabel.Location = new Point(25, 105);
            transferLabel.Size = new Size(370, 25);
            transferLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            statusGroup.Controls.Add(transferLabel);

            atmCashLabel = new Label();
            atmCashLabel.Location = new Point(25, 140);
            atmCashLabel.Size = new Size(370, 25);
            atmCashLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            statusGroup.Controls.Add(atmCashLabel);

            maxCashLabel = new Label();
            maxCashLabel.Location = new Point(25, 175);
            maxCashLabel.Size = new Size(370, 25);
            maxCashLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            statusGroup.Controls.Add(maxCashLabel);
        }

        private void BuildLogPanel()
        {
            GroupBox logGroup = new GroupBox();
            logGroup.Text = "Transaction Log";
            logGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            logGroup.Location = new Point(500, 425);
            logGroup.Size = new Size(430, 220);
            this.Controls.Add(logGroup);

            cardStatusLabel = new Label();
            cardStatusLabel.Location = new Point(20, 30);
            cardStatusLabel.Size = new Size(390, 25);
            cardStatusLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            cardStatusLabel.ForeColor = Color.FromArgb(25, 55, 95);
            logGroup.Controls.Add(cardStatusLabel);

            logBox = new ListBox();
            logBox.Location = new Point(20, 65);
            logBox.Size = new Size(390, 135);
            logGroup.Controls.Add(logBox);
        }

        private void CardTypeChanged(object sender, EventArgs e)
        {
            UpdateCardMenu();
        }

        private void UpdateCardMenu()
        {
            if (physicalRadio.Checked)
            {
                physicalInfoLabel.Text = "Physical mode: chip data is read automatically. Only PIN is entered.";

                cardNumberBox.Enabled = false;
                cv2Box.Enabled = false;
                expirationBox.Enabled = false;

                cardNumberBox.Text = "Read from chip";
                cv2Box.Text = "Read from chip";
                expirationBox.Text = "Read from chip";
            }
            else
            {
                physicalInfoLabel.Text = "Virtual mode: enter full card details manually.";

                cardNumberBox.Enabled = true;
                cv2Box.Enabled = true;
                expirationBox.Enabled = true;

                if (cardNumberBox.Text == "Read from chip")
                {
                    cardNumberBox.Text = "1234-5678-9999-0000";
                }

                if (cv2Box.Text == "Read from chip")
                {
                    cv2Box.Text = "123";
                }

                if (expirationBox.Text == "Read from chip")
                {
                    expirationBox.Text = "12/28";
                }
            }
        }

        private void CreateCardButton_Click(object sender, EventArgs e)
        {
            CardFactory factory = new CardFactory();

            Context context = new Context();
            context.SetPin(pinBox.Text);

            ProgramIF cardProgram;

            if (physicalRadio.Checked)
            {
                factory.SetCard("Physical");
                cardProgram = new PhysicalCardProgram();
            }
            else
            {
                factory.SetCard("Virtual");

                context.SetCardNumber(cardNumberBox.Text);
                context.SetCv2(cv2Box.Text);
                context.SetExpirationDate(expirationBox.Text);

                cardProgram = new VirtualCardProgram();
            }

            activeCard = factory.GetCard();
            activeCard.SetAccount(checkingAccount);

            cardProgram.SetCard(activeCard);
            cardProgram.SetContext(context);
            cardProgram.Perform();

            activeCard.SetValid();

            if (activeCard.GetValid())
            {
                AddLog("Card created and validated as " + activeCard.GetType().Name + ".");
                AddLog("Program used: " + cardProgram.GetType().Name + ".");

                if (physicalRadio.Checked)
                {
                    AddLog("Physical card chip read completed.");
                    cardNumberBox.Text = activeCard.GetCardNumber();
                    cv2Box.Text = activeCard.GetCV2();
                    expirationBox.Text = activeCard.GetExpirationDate();
                }
            }
            else
            {
                AddLog("Card validation failed.");
            }

            RefreshDisplay();
        }

        private void DepositButton_Click(object sender, EventArgs e)
        {
            if (!CardReady())
            {
                return;
            }

            if (!TryGetAmount(out decimal amount))
            {
                return;
            }

            Transaction transaction = new Transaction(activeCard);
            transaction.SetAmount(amount);
            transaction.SetData(atmData);

            if (!RunDefaultFilter(transaction))
            {
                AddLog("Deposit blocked by DefaultFilter.");
                return;
            }

            transaction.Deposit();

            if (transaction.GetStatus())
            {
                readAndWrite.SaveTransaction(transaction);
                readAndWrite.UpdateAccount(checkingAccount);
                AddLog("Deposit successful: " + amount.ToString("C"));
            }
            else
            {
                AddLog("Deposit failed.");
            }

            RefreshDisplay();
        }

        private void WithdrawButton_Click(object sender, EventArgs e)
        {
            if (!CardReady())
            {
                return;
            }

            if (!TryGetAmount(out decimal amount))
            {
                return;
            }

            Transaction transaction = new Transaction(activeCard);
            transaction.SetAmount(amount);
            transaction.SetData(atmData);

            if (!RunWithdrawalFilters(transaction))
            {
                AddLog("Withdrawal blocked by backend filters.");
                RefreshDisplay();
                return;
            }

            transaction.Withdrawal();

            if (transaction.GetStatus())
            {
                readAndWrite.SaveTransaction(transaction);
                readAndWrite.UpdateAccount(checkingAccount);
                AddLog("Withdrawal successful: " + amount.ToString("C"));
            }
            else
            {
                AddLog("Withdrawal failed.");
            }

            RefreshDisplay();
        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            if (!CardReady())
            {
                return;
            }

            if (!TryGetAmount(out decimal amount))
            {
                return;
            }

            Transaction transaction = new Transaction(activeCard);
            transaction.SetCard2(transferCard);
            transaction.SetAmount(amount);
            transaction.SetData(atmData);

            if (!RunDefaultFilter(transaction))
            {
                AddLog("Transfer blocked by DefaultFilter.");
                return;
            }

            transaction.Transfer();

            if (transaction.GetStatus())
            {
                readAndWrite.SaveTransaction(transaction);
                readAndWrite.UpdateAccount(checkingAccount);
                readAndWrite.UpdateAccount(transferAccount);
                AddLog("Transfer successful to second account: " + amount.ToString("C"));
            }
            else
            {
                AddLog("Transfer failed.");
            }

            RefreshDisplay();
        }

        private bool RunDefaultFilter(Transaction transaction)
        {
            DefaultFilter defaultFilter = new DefaultFilter();
            defaultFilter.SetTransaction(transaction);
            defaultFilter.SetData(atmData);
            defaultFilter.Register(company);
            company.AddATM(defaultFilter);

            return defaultFilter.Check();
        }

        private bool RunWithdrawalFilters(Transaction transaction)
        {
            DefaultFilter defaultFilter = new DefaultFilter();
            defaultFilter.SetTransaction(transaction);
            defaultFilter.SetData(atmData);
            defaultFilter.Register(company);
            company.AddATM(defaultFilter);

            MaximumDailyCashout maxFilter = new MaximumDailyCashout();
            maxFilter.SetTransaction(transaction);
            maxFilter.SetData(atmData);
            maxFilter.Register(company);
            company.AddATM(maxFilter);

            CheckAvailableCash cashFilter = new CheckAvailableCash();
            cashFilter.SetTransaction(transaction);
            cashFilter.SetData(atmData);
            cashFilter.Register(company);
            company.AddATM(cashFilter);

            if (!defaultFilter.Check())
            {
                AddLog("DefaultFilter failed.");
                return false;
            }

            if (!maxFilter.Check())
            {
                AddLog("MaximumDailyCashout failed. Amount is over ATM daily cashout limit.");
                return false;
            }

            if (!cashFilter.Check())
            {
                AddLog("CheckAvailableCash failed. ATM company was notified.");
                return false;
            }

            return true;
        }

        private bool CardReady()
        {
            if (activeCard == null)
            {
                MessageBox.Show("Create and validate a card first.");
                AddLog("No active card.");
                return false;
            }

            if (!activeCard.GetValid())
            {
                MessageBox.Show("The active card is invalid.");
                AddLog("Invalid card.");
                return false;
            }

            return true;
        }

        private bool TryGetAmount(out decimal amount)
        {
            amount = 0;

            if (!decimal.TryParse(amountBox.Text, out amount))
            {
                MessageBox.Show("Enter a valid number.");
                AddLog("Invalid amount input.");
                return false;
            }

            if (amount <= 0)
            {
                MessageBox.Show("Amount must be greater than 0.");
                AddLog("Amount must be greater than 0.");
                return false;
            }

            return true;
        }

        private void RefreshDisplay()
        {
            balanceLabel.Text = "Checking Balance: " + checkingAccount.GetBalance().ToString("C");
            savingsLabel.Text = "Savings Balance: " + savingsAccount.GetBalance().ToString("C");
            transferLabel.Text = "Transfer Account Balance: " + transferAccount.GetBalance().ToString("C");
            atmCashLabel.Text = "ATM Available Cash: " + atmData.GetCash().ToString("C");
            maxCashLabel.Text = "ATM Max Cashout: " + atmData.GetMaximumCashOut().ToString("C");

            if (activeCard == null)
            {
                cardStatusLabel.Text = "Card Status: No card created";
            }
            else if (activeCard.GetValid())
            {
                cardStatusLabel.Text = "Card Status: Valid " + activeCard.GetType().Name + " Card";
            }
            else
            {
                cardStatusLabel.Text = "Card Status: Invalid";
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshDisplay();
            AddLog("Status refreshed.");
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            logBox.Items.Clear();
        }

        private void AddLog(string message)
        {
            logBox.Items.Insert(0, DateTime.Now.ToString("hh:mm:ss tt") + " - " + message);
        }
    }
}