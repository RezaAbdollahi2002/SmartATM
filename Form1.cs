using FinalProject.ATMPackage;
using FinalProject.CardPackage;
using FinalProject.TransactionPackage;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        private Account mainAccount;
        private Account transferAccount;
        private CardIF currentCard;
        private CardIF transferCard;
        private Data atmData;

        private Label titleLabel;
        private Label balanceLabel;
        private Label atmCashLabel;
        private Label statusLabel;

        private TextBox pinBox;
        private TextBox cardNumberBox;
        private TextBox cv2Box;
        private TextBox expBox;
        private TextBox amountBox;

        private RadioButton physicalRadio;
        private RadioButton virtualRadio;

        private Button createCardButton;
        private Button depositButton;
        private Button withdrawButton;
        private Button transferButton;
        private Button clearButton;

        private ListBox logBox;

        public Form1()
        {
            InitializeComponent();
            SetupBackend();
            BuildFrontend();
            RefreshDisplay();
        }

        private void SetupBackend()
        {
            mainAccount = new Account();
            mainAccount.SetAccountId("ACC-1001");
            mainAccount.SetBalance(500.00m);

            transferAccount = new Account();
            transferAccount.SetAccountId("ACC-2002");
            transferAccount.SetBalance(250.00m);

            atmData = new Data();
            atmData.SetCash(2000.00m);
            atmData.SetMaximumCashOut(500.00m);
        }

        private void BuildFrontend()
        {
            this.Text = "SmartATM";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(235, 240, 245);

            titleLabel = new Label();
            titleLabel.Text = "SmartATM Banking System";
            titleLabel.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(25, 55, 95);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(30, 25);
            this.Controls.Add(titleLabel);

            GroupBox cardGroup = new GroupBox();
            cardGroup.Text = "Card Information";
            cardGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            cardGroup.Location = new Point(30, 90);
            cardGroup.Size = new Size(390, 250);
            this.Controls.Add(cardGroup);

            physicalRadio = new RadioButton();
            physicalRadio.Text = "Physical Card";
            physicalRadio.Location = new Point(20, 35);
            physicalRadio.Size = new Size(150, 25);
            physicalRadio.Checked = true;
            cardGroup.Controls.Add(physicalRadio);

            virtualRadio = new RadioButton();
            virtualRadio.Text = "Virtual Card";
            virtualRadio.Location = new Point(180, 35);
            virtualRadio.Size = new Size(150, 25);
            cardGroup.Controls.Add(virtualRadio);

            Label pinLabel = new Label();
            pinLabel.Text = "PIN:";
            pinLabel.Location = new Point(20, 75);
            pinLabel.Size = new Size(120, 25);
            cardGroup.Controls.Add(pinLabel);

            pinBox = new TextBox();
            pinBox.Location = new Point(150, 75);
            pinBox.Size = new Size(200, 25);
            pinBox.Text = "1234";
            cardGroup.Controls.Add(pinBox);

            Label cardNumberLabel = new Label();
            cardNumberLabel.Text = "Card Number:";
            cardNumberLabel.Location = new Point(20, 110);
            cardNumberLabel.Size = new Size(120, 25);
            cardGroup.Controls.Add(cardNumberLabel);

            cardNumberBox = new TextBox();
            cardNumberBox.Location = new Point(150, 110);
            cardNumberBox.Size = new Size(200, 25);
            cardNumberBox.Text = "1234-5678-9999";
            cardGroup.Controls.Add(cardNumberBox);

            Label cv2Label = new Label();
            cv2Label.Text = "CV2:";
            cv2Label.Location = new Point(20, 145);
            cv2Label.Size = new Size(120, 25);
            cardGroup.Controls.Add(cv2Label);

            cv2Box = new TextBox();
            cv2Box.Location = new Point(150, 145);
            cv2Box.Size = new Size(200, 25);
            cv2Box.Text = "123";
            cardGroup.Controls.Add(cv2Box);

            Label expLabel = new Label();
            expLabel.Text = "Expiration:";
            expLabel.Location = new Point(20, 180);
            expLabel.Size = new Size(120, 25);
            cardGroup.Controls.Add(expLabel);

            expBox = new TextBox();
            expBox.Location = new Point(150, 180);
            expBox.Size = new Size(200, 25);
            expBox.Text = "12/28";
            cardGroup.Controls.Add(expBox);

            createCardButton = new Button();
            createCardButton.Text = "Create / Validate Card";
            createCardButton.Location = new Point(95, 215);
            createCardButton.Size = new Size(200, 30);
            createCardButton.BackColor = Color.FromArgb(40, 100, 180);
            createCardButton.ForeColor = Color.White;
            createCardButton.FlatStyle = FlatStyle.Flat;
            createCardButton.Click += CreateCardButton_Click;
            cardGroup.Controls.Add(createCardButton);

            GroupBox transactionGroup = new GroupBox();
            transactionGroup.Text = "Transactions";
            transactionGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            transactionGroup.Location = new Point(450, 90);
            transactionGroup.Size = new Size(430, 250);
            this.Controls.Add(transactionGroup);

            Label amountLabel = new Label();
            amountLabel.Text = "Amount:";
            amountLabel.Location = new Point(25, 45);
            amountLabel.Size = new Size(100, 25);
            transactionGroup.Controls.Add(amountLabel);

            amountBox = new TextBox();
            amountBox.Location = new Point(130, 45);
            amountBox.Size = new Size(240, 25);
            amountBox.Text = "50";
            transactionGroup.Controls.Add(amountBox);

            depositButton = new Button();
            depositButton.Text = "Deposit";
            depositButton.Location = new Point(30, 95);
            depositButton.Size = new Size(110, 45);
            depositButton.BackColor = Color.FromArgb(45, 145, 75);
            depositButton.ForeColor = Color.White;
            depositButton.FlatStyle = FlatStyle.Flat;
            depositButton.Click += DepositButton_Click;
            transactionGroup.Controls.Add(depositButton);

            withdrawButton = new Button();
            withdrawButton.Text = "Withdraw";
            withdrawButton.Location = new Point(160, 95);
            withdrawButton.Size = new Size(110, 45);
            withdrawButton.BackColor = Color.FromArgb(200, 120, 40);
            withdrawButton.ForeColor = Color.White;
            withdrawButton.FlatStyle = FlatStyle.Flat;
            withdrawButton.Click += WithdrawButton_Click;
            transactionGroup.Controls.Add(withdrawButton);

            transferButton = new Button();
            transferButton.Text = "Transfer";
            transferButton.Location = new Point(290, 95);
            transferButton.Size = new Size(110, 45);
            transferButton.BackColor = Color.FromArgb(90, 90, 190);
            transferButton.ForeColor = Color.White;
            transferButton.FlatStyle = FlatStyle.Flat;
            transferButton.Click += TransferButton_Click;
            transactionGroup.Controls.Add(transferButton);

            clearButton = new Button();
            clearButton.Text = "Clear Log";
            clearButton.Location = new Point(130, 165);
            clearButton.Size = new Size(170, 35);
            clearButton.Click += ClearButton_Click;
            transactionGroup.Controls.Add(clearButton);

            GroupBox infoGroup = new GroupBox();
            infoGroup.Text = "ATM Status";
            infoGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            infoGroup.Location = new Point(30, 365);
            infoGroup.Size = new Size(390, 190);
            this.Controls.Add(infoGroup);

            balanceLabel = new Label();
            balanceLabel.Location = new Point(25, 40);
            balanceLabel.Size = new Size(330, 30);
            balanceLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            infoGroup.Controls.Add(balanceLabel);

            atmCashLabel = new Label();
            atmCashLabel.Location = new Point(25, 80);
            atmCashLabel.Size = new Size(330, 30);
            atmCashLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            infoGroup.Controls.Add(atmCashLabel);

            statusLabel = new Label();
            statusLabel.Location = new Point(25, 120);
            statusLabel.Size = new Size(330, 45);
            statusLabel.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            statusLabel.ForeColor = Color.FromArgb(25, 55, 95);
            infoGroup.Controls.Add(statusLabel);

            GroupBox logGroup = new GroupBox();
            logGroup.Text = "Transaction Log";
            logGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            logGroup.Location = new Point(450, 365);
            logGroup.Size = new Size(430, 190);
            this.Controls.Add(logGroup);

            logBox = new ListBox();
            logBox.Location = new Point(20, 30);
            logBox.Size = new Size(390, 140);
            logGroup.Controls.Add(logBox);
        }

        private void CreateCardButton_Click(object sender, EventArgs e)
        {
            if (physicalRadio.Checked)
            {
                currentCard = new Physical();
            }
            else
            {
                currentCard = new Virtual();
            }

            currentCard.SetAccount(mainAccount);
            currentCard.SetPIN(pinBox.Text);
            currentCard.SetCardNumber(cardNumberBox.Text);
            currentCard.SetCV2(cv2Box.Text);
            currentCard.SetExpirationDate(expBox.Text);
            currentCard.SetValid();

            transferCard = new Virtual();
            transferCard.SetAccount(transferAccount);
            transferCard.SetPIN("9999");
            transferCard.SetCardNumber("9999-8888-7777");
            transferCard.SetCV2("555");
            transferCard.SetExpirationDate("01/29");
            transferCard.SetValid();

            if (currentCard.GetValid())
            {
                AddLog("Card created and validated successfully.");
                statusLabel.Text = "Card status: Valid";
            }
            else
            {
                AddLog("Card is invalid. Check the entered card information.");
                statusLabel.Text = "Card status: Invalid";
            }

            RefreshDisplay();
        }

        private void DepositButton_Click(object sender, EventArgs e)
        {
            if (!CardReady())
                return;

            if (!TryGetAmount(out decimal amount))
                return;

            Transaction transaction = new Transaction(currentCard);
            transaction.SetAmount(amount);
            transaction.SetData(atmData);
            transaction.Deposit();

            if (transaction.GetStatus())
            {
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
                return;

            if (!TryGetAmount(out decimal amount))
                return;

            Transaction transaction = new Transaction(currentCard);
            transaction.SetAmount(amount);
            transaction.SetData(atmData);
            transaction.Withdrawal();

            if (transaction.GetStatus())
            {
                AddLog("Withdrawal successful: " + amount.ToString("C"));
            }
            else
            {
                AddLog("Withdrawal failed. Check balance, ATM cash, or daily max.");
            }

            RefreshDisplay();
        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            if (!CardReady())
                return;

            if (!TryGetAmount(out decimal amount))
                return;

            if (transferCard == null)
            {
                CreateCardButton_Click(sender, e);
            }

            Transaction transaction = new Transaction(currentCard);
            transaction.SetCard2(transferCard);
            transaction.SetAmount(amount);
            transaction.Transfer();

            if (transaction.GetStatus())
            {
                AddLog("Transfer successful: " + amount.ToString("C"));
            }
            else
            {
                AddLog("Transfer failed. Check balance or card status.");
            }

            RefreshDisplay();
        }

        private bool CardReady()
        {
            if (currentCard == null)
            {
                MessageBox.Show("Create and validate a card first.");
                AddLog("Transaction blocked: no card created.");
                return false;
            }

            if (!currentCard.GetValid())
            {
                MessageBox.Show("The current card is invalid.");
                AddLog("Transaction blocked: invalid card.");
                return false;
            }

            return true;
        }

        private bool TryGetAmount(out decimal amount)
        {
            amount = 0;

            if (!decimal.TryParse(amountBox.Text, out amount))
            {
                MessageBox.Show("Enter a valid number for the amount.");
                AddLog("Invalid amount entered.");
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
            balanceLabel.Text = "Main Account Balance: " + mainAccount.GetBalance().ToString("C");
            atmCashLabel.Text = "ATM Available Cash: " + atmData.GetCash().ToString("C");

            if (currentCard == null)
            {
                statusLabel.Text = "Card status: No card created yet.";
            }
        }

        private void AddLog(string message)
        {
            logBox.Items.Insert(0, DateTime.Now.ToString("hh:mm:ss tt") + " - " + message);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            logBox.Items.Clear();
        }
    }
}