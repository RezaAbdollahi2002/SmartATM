using FinalProject.ATMPackage;
using FinalProject.CardPackage;
using FinalProject.CompanyPackage;
using FinalProject.FilterPackage;
using FinalProject.ProgramPackage;
using FinalProject.TransactionPackage;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        private Account checkingAccount;
        private Account savingsAccount;
        private Account externalTransferAccount;

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
        private Label selectedAccountLabel;

        private TextBox pinBox;
        private TextBox cardNumberBox;
        private TextBox cv2Box;
        private TextBox expirationBox;
        private TextBox amountBox;

        private ComboBox sourceAccountBox;
        private ComboBox transferTargetBox;

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

            externalTransferAccount = new Account();
            externalTransferAccount.SetAccountId("EXTERNAL-2001");
            externalTransferAccount.SetBalance(300.00m);

            atmData = new Data();
            atmData.SetCash(2500.00m);
            atmData.SetMaximumCashOut(500.00m);

            company = new Company();
            company.SetData(atmData);

            readAndWrite = new ReadAndWrite();
            readAndWrite.SaveAccount(checkingAccount);
            readAndWrite.SaveAccount(savingsAccount);
            readAndWrite.SaveAccount(externalTransferAccount);
        }

        private void BuildFrontend()
        {
            this.Controls.Clear();
            this.Text = "SmartATM";
            this.ClientSize = new Size(1040, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(235, 240, 245);
            this.Font = new Font("Segoe UI", 9);

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
            cardGroup.Text = "Card Program / Factory";
            cardGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            cardGroup.Location = new Point(30, 85);
            cardGroup.Size = new Size(455, 345);
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
            virtualRadio.Location = new Point(225, 35);
            virtualRadio.Size = new Size(160, 25);
            virtualRadio.CheckedChanged += CardTypeChanged;
            cardGroup.Controls.Add(virtualRadio);

            physicalInfoLabel = new Label();
            physicalInfoLabel.Location = new Point(25, 65);
            physicalInfoLabel.Size = new Size(390, 35);
            physicalInfoLabel.ForeColor = Color.FromArgb(25, 55, 95);
            physicalInfoLabel.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            cardGroup.Controls.Add(physicalInfoLabel);

            Label accountLabel = new Label();
            accountLabel.Text = "Source Account:";
            accountLabel.Location = new Point(25, 110);
            accountLabel.Size = new Size(130, 25);
            cardGroup.Controls.Add(accountLabel);

            sourceAccountBox = new ComboBox();
            sourceAccountBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sourceAccountBox.Location = new Point(175, 108);
            sourceAccountBox.Size = new Size(220, 25);
            sourceAccountBox.Items.Add("Checking");
            sourceAccountBox.Items.Add("Savings");
            sourceAccountBox.SelectedIndex = 0;
            sourceAccountBox.SelectedIndexChanged += SourceAccountBox_SelectedIndexChanged;
            cardGroup.Controls.Add(sourceAccountBox);

            Label pinLabel = new Label();
            pinLabel.Text = "PIN:";
            pinLabel.Location = new Point(25, 145);
            pinLabel.Size = new Size(130, 25);
            cardGroup.Controls.Add(pinLabel);

            pinBox = new TextBox();
            pinBox.Location = new Point(175, 145);
            pinBox.Size = new Size(220, 25);
            pinBox.Text = "1234";
            cardGroup.Controls.Add(pinBox);

            Label numberLabel = new Label();
            numberLabel.Text = "Card Number:";
            numberLabel.Location = new Point(25, 180);
            numberLabel.Size = new Size(130, 25);
            cardGroup.Controls.Add(numberLabel);

            cardNumberBox = new TextBox();
            cardNumberBox.Location = new Point(175, 180);
            cardNumberBox.Size = new Size(220, 25);
            cardNumberBox.Text = "1234-5678-9999-0000";
            cardGroup.Controls.Add(cardNumberBox);

            Label cv2Label = new Label();
            cv2Label.Text = "CV2:";
            cv2Label.Location = new Point(25, 215);
            cv2Label.Size = new Size(130, 25);
            cardGroup.Controls.Add(cv2Label);

            cv2Box = new TextBox();
            cv2Box.Location = new Point(175, 215);
            cv2Box.Size = new Size(220, 25);
            cv2Box.Text = "123";
            cardGroup.Controls.Add(cv2Box);

            Label expLabel = new Label();
            expLabel.Text = "Expiration:";
            expLabel.Location = new Point(25, 250);
            expLabel.Size = new Size(130, 25);
            cardGroup.Controls.Add(expLabel);

            expirationBox = new TextBox();
            expirationBox.Location = new Point(175, 250);
            expirationBox.Size = new Size(220, 25);
            expirationBox.Text = "12/28";
            cardGroup.Controls.Add(expirationBox);

            createCardButton = new Button();
            createCardButton.Text = "Create / Validate Card";
            createCardButton.Location = new Point(115, 295);
            createCardButton.Size = new Size(220, 35);
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
            transactionGroup.Location = new Point(525, 85);
            transactionGroup.Size = new Size(455, 345);
            this.Controls.Add(transactionGroup);

            Label amountLabel = new Label();
            amountLabel.Text = "Amount:";
            amountLabel.Location = new Point(25, 45);
            amountLabel.Size = new Size(130, 25);
            transactionGroup.Controls.Add(amountLabel);

            amountBox = new TextBox();
            amountBox.Location = new Point(175, 45);
            amountBox.Size = new Size(220, 25);
            amountBox.Text = "50";
            transactionGroup.Controls.Add(amountBox);

            Label targetLabel = new Label();
            targetLabel.Text = "Transfer Target:";
            targetLabel.Location = new Point(25, 85);
            targetLabel.Size = new Size(130, 25);
            transactionGroup.Controls.Add(targetLabel);

            transferTargetBox = new ComboBox();
            transferTargetBox.DropDownStyle = ComboBoxStyle.DropDownList;
            transferTargetBox.Location = new Point(175, 83);
            transferTargetBox.Size = new Size(220, 25);
            transferTargetBox.Items.Add("Savings");
            transferTargetBox.Items.Add("Checking");
            transferTargetBox.Items.Add("External Transfer Account");
            transferTargetBox.SelectedIndex = 0;
            transactionGroup.Controls.Add(transferTargetBox);

            depositButton = new Button();
            depositButton.Text = "Deposit";
            depositButton.Location = new Point(30, 135);
            depositButton.Size = new Size(120, 50);
            depositButton.BackColor = Color.FromArgb(45, 145, 75);
            depositButton.ForeColor = Color.White;
            depositButton.FlatStyle = FlatStyle.Flat;
            depositButton.Click += DepositButton_Click;
            transactionGroup.Controls.Add(depositButton);

            withdrawButton = new Button();
            withdrawButton.Text = "Withdraw";
            withdrawButton.Location = new Point(168, 135);
            withdrawButton.Size = new Size(120, 50);
            withdrawButton.BackColor = Color.FromArgb(200, 120, 40);
            withdrawButton.ForeColor = Color.White;
            withdrawButton.FlatStyle = FlatStyle.Flat;
            withdrawButton.Click += WithdrawButton_Click;
            transactionGroup.Controls.Add(withdrawButton);

            transferButton = new Button();
            transferButton.Text = "Transfer";
            transferButton.Location = new Point(306, 135);
            transferButton.Size = new Size(120, 50);
            transferButton.BackColor = Color.FromArgb(90, 90, 190);
            transferButton.ForeColor = Color.White;
            transferButton.FlatStyle = FlatStyle.Flat;
            transferButton.Click += TransferButton_Click;
            transactionGroup.Controls.Add(transferButton);

            refreshButton = new Button();
            refreshButton.Text = "Refresh Status";
            refreshButton.Location = new Point(75, 225);
            refreshButton.Size = new Size(135, 40);
            refreshButton.Click += RefreshButton_Click;
            transactionGroup.Controls.Add(refreshButton);

            clearLogButton = new Button();
            clearLogButton.Text = "Clear Log";
            clearLogButton.Location = new Point(235, 225);
            clearLogButton.Size = new Size(135, 40);
            clearLogButton.Click += ClearLogButton_Click;
            transactionGroup.Controls.Add(clearLogButton);

            selectedAccountLabel = new Label();
            selectedAccountLabel.Location = new Point(25, 292);
            selectedAccountLabel.Size = new Size(390, 25);
            selectedAccountLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            selectedAccountLabel.ForeColor = Color.FromArgb(25, 55, 95);
            transactionGroup.Controls.Add(selectedAccountLabel);
        }

        private void BuildStatusPanel()
        {
            GroupBox statusGroup = new GroupBox();
            statusGroup.Text = "Backend Status";
            statusGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            statusGroup.Location = new Point(30, 455);
            statusGroup.Size = new Size(455, 220);
            this.Controls.Add(statusGroup);

            balanceLabel = MakeStatusLabel(25, 35);
            statusGroup.Controls.Add(balanceLabel);

            savingsLabel = MakeStatusLabel(25, 70);
            statusGroup.Controls.Add(savingsLabel);

            transferLabel = MakeStatusLabel(25, 105);
            statusGroup.Controls.Add(transferLabel);

            atmCashLabel = MakeStatusLabel(25, 140);
            statusGroup.Controls.Add(atmCashLabel);

            maxCashLabel = MakeStatusLabel(25, 175);
            statusGroup.Controls.Add(maxCashLabel);
        }

        private Label MakeStatusLabel(int x, int y)
        {
            Label label = new Label();
            label.Location = new Point(x, y);
            label.Size = new Size(395, 25);
            label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            return label;
        }

        private void BuildLogPanel()
        {
            GroupBox logGroup = new GroupBox();
            logGroup.Text = "Transaction Log / ReadAndWrite Storage";
            logGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            logGroup.Location = new Point(525, 455);
            logGroup.Size = new Size(455, 220);
            this.Controls.Add(logGroup);

            cardStatusLabel = new Label();
            cardStatusLabel.Location = new Point(20, 30);
            cardStatusLabel.Size = new Size(410, 25);
            cardStatusLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            cardStatusLabel.ForeColor = Color.FromArgb(25, 55, 95);
            logGroup.Controls.Add(cardStatusLabel);

            logBox = new ListBox();
            logBox.Location = new Point(20, 65);
            logBox.Size = new Size(410, 135);
            logGroup.Controls.Add(logBox);
        }

        private void CardTypeChanged(object sender, EventArgs e)
        {
            UpdateCardMenu();
        }

        private void SourceAccountBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (activeCard != null)
            {
                activeCard.SetAccount(GetSourceAccount());
                AddLog("Active card source account changed to " + GetSourceAccount().GetAccountId() + ".");
            }

            RefreshDisplay();
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

                if (cardNumberBox.Text == "Read from chip") cardNumberBox.Text = "1234-5678-9999-0000";
                if (cv2Box.Text == "Read from chip") cv2Box.Text = "123";
                if (expirationBox.Text == "Read from chip") expirationBox.Text = "12/28";
            }
        }

        private void CreateCardButton_Click(object sender, EventArgs e)
        {
            CardFactory factory = new CardFactory();
            Context context = new Context();
            context.SetPin(pinBox.Text.Trim());

            ProgramIF cardProgram;

            if (physicalRadio.Checked)
            {
                factory.SetCard("Physical");
                cardProgram = new PhysicalCardProgram();
            }
            else
            {
                factory.SetCard("Virtual");
                context.SetCardNumber(cardNumberBox.Text.Trim());
                context.SetCv2(cv2Box.Text.Trim());
                context.SetExpirationDate(expirationBox.Text.Trim());
                cardProgram = new VirtualCardProgram();
            }

            activeCard = factory.GetCard();
            activeCard.SetAccount(GetSourceAccount());

            cardProgram.SetCard(activeCard);
            cardProgram.SetContext(context);
            cardProgram.Perform();
            activeCard.SetValid();

            if (activeCard.GetValid())
            {
                cardNumberBox.Text = activeCard.GetCardNumber();
                cv2Box.Text = activeCard.GetCV2();
                expirationBox.Text = activeCard.GetExpirationDate();

                AddLog("CardFactory created a " + activeCard.GetType().Name + " card.");
                AddLog(cardProgram.GetType().Name + " loaded card details into the card object.");
                AddLog("Card is valid and connected to " + activeCard.GetAccount().GetAccountId() + ".");
            }
            else
            {
                AddLog("Card validation failed. Check PIN/card data.");
            }

            RefreshDisplay();
        }

        private void DepositButton_Click(object sender, EventArgs e)
        {
            if (!PrepareTransaction(out Transaction transaction)) return;

            if (!RunDefaultFilter(transaction))
            {
                AddLog("Deposit blocked by DefaultFilter.");
                return;
            }

            transaction.Deposit();
            FinishTransaction(transaction, "Deposit", transaction.GetStatus());
        }

        private void WithdrawButton_Click(object sender, EventArgs e)
        {
            if (!PrepareTransaction(out Transaction transaction)) return;

            if (!RunWithdrawalFilterChain(transaction))
            {
                AddLog("Withdrawal blocked by DefaultFilter, MaximumDailyCashout, or CheckAvailableCash.");
                RefreshDisplay();
                return;
            }

            transaction.Withdrawal();
            FinishTransaction(transaction, "Withdrawal", transaction.GetStatus());
        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            if (!PrepareTransaction(out Transaction transaction)) return;

            Account targetAccount = GetTransferTargetAccount();
            if (targetAccount == GetSourceAccount())
            {
                MessageBox.Show("Transfer target must be different from the source account.");
                AddLog("Transfer failed because source and target were the same account.");
                return;
            }

            transferCard = BuildTransferCard(targetAccount);
            transaction.SetCard2(transferCard);

            if (!RunDefaultFilter(transaction))
            {
                AddLog("Transfer blocked by DefaultFilter.");
                return;
            }

            transaction.Transfer();
            FinishTransaction(transaction, "Transfer", transaction.GetStatus());
        }

        private bool PrepareTransaction(out Transaction transaction)
        {
            transaction = null;

            if (!CardReady()) return false;
            if (!TryGetAmount(out decimal amount)) return false;

            activeCard.SetAccount(GetSourceAccount());

            transaction = new Transaction(activeCard);
            transaction.SetAmount(amount);
            transaction.SetData(atmData);
            return true;
        }

        private void FinishTransaction(Transaction transaction, string type, bool success)
        {
            if (success)
            {
                readAndWrite.SaveTransaction(transaction);
                readAndWrite.UpdateAccount(checkingAccount);
                readAndWrite.UpdateAccount(savingsAccount);
                readAndWrite.UpdateAccount(externalTransferAccount);

                Transaction saved = readAndWrite.ReadTransaction(transaction.GetTransactionId());
                string shortId = saved == null ? "not saved" : saved.GetTransactionId().Substring(0, 8);
                AddLog(type + " successful: " + transaction.GetAmount().ToString("C") + " | Saved ID: " + shortId);
            }
            else
            {
                AddLog(type + " failed. Check card, account balance, ATM cash, or limits.");
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

        private bool RunWithdrawalFilterChain(Transaction transaction)
        {
            DefaultFilter defaultFilter = new DefaultFilter();
            MaximumDailyCashout maxFilter = new MaximumDailyCashout();
            CheckAvailableCash cashFilter = new CheckAvailableCash();

            defaultFilter.SetFilter(maxFilter);
            maxFilter.SetFilter(cashFilter);

            defaultFilter.SetTransaction(transaction);
            defaultFilter.SetData(atmData);
            defaultFilter.Register(company);

            company.AddATM(defaultFilter);
            company.AddATM(maxFilter);
            company.AddATM(cashFilter);

            return defaultFilter.Check();
        }

        private CardIF BuildTransferCard(Account targetAccount)
        {
            CardIF card = new Virtual();
            card.SetAccount(targetAccount);
            card.SetPIN("9999");
            card.SetCardNumber("9999-8888-7777-6666");
            card.SetCV2("555");
            card.SetExpirationDate("01/29");
            card.SetValid();
            return card;
        }

        private Account GetSourceAccount()
        {
            if (sourceAccountBox != null && sourceAccountBox.SelectedItem != null && sourceAccountBox.SelectedItem.ToString() == "Savings")
            {
                return savingsAccount;
            }

            return checkingAccount;
        }

        private Account GetTransferTargetAccount()
        {
            string selected = transferTargetBox.SelectedItem == null ? "Savings" : transferTargetBox.SelectedItem.ToString();

            if (selected == "Checking") return checkingAccount;
            if (selected == "External Transfer Account") return externalTransferAccount;
            return savingsAccount;
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
            Account storedChecking = readAndWrite.ReadAccount("CHECKING-1001") ?? checkingAccount;
            Account storedSavings = readAndWrite.ReadAccount("SAVINGS-1002") ?? savingsAccount;
            Account storedExternal = readAndWrite.ReadAccount("EXTERNAL-2001") ?? externalTransferAccount;

            balanceLabel.Text = "Checking Balance: " + storedChecking.GetBalance().ToString("C");
            savingsLabel.Text = "Savings Balance: " + storedSavings.GetBalance().ToString("C");
            transferLabel.Text = "External Transfer Balance: " + storedExternal.GetBalance().ToString("C");
            atmCashLabel.Text = "ATM Available Cash: " + atmData.GetCash().ToString("C");
            maxCashLabel.Text = "ATM Max Cashout: " + atmData.GetMaximumCashOut().ToString("C");

            if (selectedAccountLabel != null)
            {
                selectedAccountLabel.Text = "Selected Source: " + GetSourceAccount().GetAccountId();
            }

            if (cardStatusLabel != null)
            {
                if (activeCard == null)
                {
                    cardStatusLabel.Text = "Card Status: No card created";
                }
                else if (activeCard.GetValid())
                {
                    cardStatusLabel.Text = "Card Status: Valid " + activeCard.GetType().Name + " Card | " + activeCard.GetAccount().GetAccountId();
                }
                else
                {
                    cardStatusLabel.Text = "Card Status: Invalid";
                }
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshDisplay();
            AddLog("Status refreshed. Company observes " + company.GetATM().Count + " filter object(s).");
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            logBox.Items.Clear();
        }

        private void AddLog(string message)
        {
            if (logBox != null)
            {
                logBox.Items.Insert(0, DateTime.Now.ToString("hh:mm:ss tt") + " - " + message);
            }
        }
    }
}