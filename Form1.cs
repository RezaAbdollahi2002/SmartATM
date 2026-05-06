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
        private const string CheckingAccountId = "CHECKING-1001";
        private const string SavingsAccountId = "SAVINGS-1002";
        private const string ExternalAccountId = "EXTERNAL-2001";

        private static bool backendInitialized = false;

        private static Account checkingAccount;
        private static Account savingsAccount;
        private static Account externalTransferAccount;

        private static Data atmData;
        private static Company company;

        private readonly string userName;

        private CardIF activeCard;
        private CardIF transferCard;

        private Label balanceLabel;
        private Label savingsLabel;
        private Label transferLabel;
        private Label atmCashLabel;
        private Label maxCashLabel;
        private Label cardStatusLabel;
        private Label selectedAccountLabel;
        private Label cardModeLabel;

        private Panel physicalCardPanel;
        private Panel virtualCardPanel;

        private TextBox pinBox;
        private TextBox cardNumberBox;
        private TextBox cv2Box;
        private TextBox expirationBox;
        private TextBox amountBox;

        private ComboBox sourceAccountBox;
        private ComboBox transferTargetBox;

        private RadioButton physicalRadio;
        private RadioButton virtualRadio;

        private ListBox logBox;

        public Form1() : this("User")
        {
        }

        public Form1(string userName)
        {
            this.userName = userName;

            InitializeComponent();
            InitializeSharedBackend();
            BuildFrontend();
            UpdateCardView();
            RefreshDisplay();
        }

        private static void InitializeSharedBackend()
        {
            if (backendInitialized)
                return;

            checkingAccount = CreateAccount(CheckingAccountId, 750.00m);
            savingsAccount = CreateAccount(SavingsAccountId, 1200.00m);
            externalTransferAccount = CreateAccount(ExternalAccountId, 300.00m);

            atmData = new Data();
            atmData.SetCash(2500.00m);
            atmData.SetMaximumCashOut(500.00m);

            company = new Company();
            company.SetData(atmData);

            atmData.Register(company);
            company.AddATM(atmData);

            backendInitialized = true;
        }

        private static Account CreateAccount(string accountId, decimal balance)
        {
            Account account = new Account();
            account.SetAccountId(accountId);
            account.SetBalance(balance);
            return account;
        }

        private void BuildFrontend()
        {
            Controls.Clear();

            Text = $"SmartATM - {userName}";
            ClientSize = new Size(1040, 720);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(235, 240, 245);
            Font = new Font("Segoe UI", 9);

            Label titleLabel = new Label
            {
                Text = $"SmartATM Banking System - {userName}",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95),
                AutoSize = true,
                Location = new Point(30, 20)
            };

            Controls.Add(titleLabel);

            BuildCardPanel();
            BuildTransactionPanel();
            BuildStatusPanel();
            BuildLogPanel();
        }

        private void BuildCardPanel()
        {
            GroupBox cardGroup = CreateGroupBox("Card Program / Factory", 30, 85, 455, 345);

            physicalRadio = new RadioButton
            {
                Text = "Physical Card",
                Location = new Point(25, 35),
                Size = new Size(160, 25),
                Checked = true
            };
            physicalRadio.CheckedChanged += CardTypeChanged;

            virtualRadio = new RadioButton
            {
                Text = "Virtual Card",
                Location = new Point(225, 35),
                Size = new Size(160, 25)
            };
            virtualRadio.CheckedChanged += CardTypeChanged;

            cardModeLabel = new Label
            {
                Location = new Point(25, 65),
                Size = new Size(390, 35),
                ForeColor = Color.FromArgb(25, 55, 95),
                Font = new Font("Segoe UI", 9, FontStyle.Italic)
            };

            sourceAccountBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(175, 108),
                Size = new Size(220, 25)
            };
            sourceAccountBox.Items.Add("Checking");
            sourceAccountBox.Items.Add("Savings");
            sourceAccountBox.SelectedIndex = 0;
            sourceAccountBox.SelectedIndexChanged += SourceAccountChanged;

            pinBox = new TextBox
            {
                Location = new Point(175, 145),
                Size = new Size(220, 25),
                Text = "1234"
            };

            BuildPhysicalCardView(cardGroup);
            BuildVirtualCardView(cardGroup);

            Button createCardButton = CreateColoredButton(
                "Create / Validate Card",
                115,
                295,
                220,
                35,
                Color.FromArgb(40, 100, 180)
            );
            createCardButton.Click += CreateCardButton_Click;

            cardGroup.Controls.Add(physicalRadio);
            cardGroup.Controls.Add(virtualRadio);
            cardGroup.Controls.Add(cardModeLabel);
            cardGroup.Controls.Add(CreateFieldLabel("Source Account:", 25, 110));
            cardGroup.Controls.Add(sourceAccountBox);
            cardGroup.Controls.Add(CreateFieldLabel("PIN:", 25, 145));
            cardGroup.Controls.Add(pinBox);
            cardGroup.Controls.Add(createCardButton);
        }

        private void BuildPhysicalCardView(GroupBox cardGroup)
        {
            physicalCardPanel = new Panel
            {
                Location = new Point(25, 180),
                Size = new Size(395, 95),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(245, 248, 252)
            };

            Label title = new Label
            {
                Text = "Physical Card View",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95),
                Location = new Point(15, 10),
                Size = new Size(250, 25)
            };

            Label info = new Label
            {
                Text = "Card number, CV2, and expiration are read automatically from the card chip.",
                Location = new Point(15, 40),
                Size = new Size(350, 40)
            };

            physicalCardPanel.Controls.Add(title);
            physicalCardPanel.Controls.Add(info);
            cardGroup.Controls.Add(physicalCardPanel);
        }

        private void BuildVirtualCardView(GroupBox cardGroup)
        {
            virtualCardPanel = new Panel
            {
                Location = new Point(25, 175),
                Size = new Size(395, 110),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 255)
            };

            cardNumberBox = CreateSmallPanelTextBox(135, 8, "1234-5678-9999-0000");
            cv2Box = CreateSmallPanelTextBox(135, 40, "123");
            expirationBox = CreateSmallPanelTextBox(135, 72, "12/28");

            virtualCardPanel.Controls.Add(CreateSmallPanelLabel("Card Number:", 10, 10));
            virtualCardPanel.Controls.Add(cardNumberBox);
            virtualCardPanel.Controls.Add(CreateSmallPanelLabel("CV2:", 10, 42));
            virtualCardPanel.Controls.Add(cv2Box);
            virtualCardPanel.Controls.Add(CreateSmallPanelLabel("Expiration:", 10, 74));
            virtualCardPanel.Controls.Add(expirationBox);

            cardGroup.Controls.Add(virtualCardPanel);
        }

        private void BuildTransactionPanel()
        {
            GroupBox transactionGroup = CreateGroupBox("Transaction Controls", 525, 85, 455, 345);

            amountBox = new TextBox
            {
                Location = new Point(175, 45),
                Size = new Size(220, 25),
                Text = "50"
            };

            transferTargetBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(175, 83),
                Size = new Size(220, 25)
            };
            transferTargetBox.Items.Add("Savings");
            transferTargetBox.Items.Add("Checking");
            transferTargetBox.Items.Add("External Transfer Account");
            transferTargetBox.SelectedIndex = 0;

            Button depositButton = CreateColoredButton("Deposit", 30, 135, 120, 50, Color.FromArgb(45, 145, 75));
            depositButton.Click += DepositButton_Click;

            Button withdrawButton = CreateColoredButton("Withdraw", 168, 135, 120, 50, Color.FromArgb(200, 120, 40));
            withdrawButton.Click += WithdrawButton_Click;

            Button transferButton = CreateColoredButton("Transfer", 306, 135, 120, 50, Color.FromArgb(90, 90, 190));
            transferButton.Click += TransferButton_Click;

            Button refreshButton = new Button
            {
                Text = "Refresh Status",
                Location = new Point(75, 225),
                Size = new Size(135, 40)
            };
            refreshButton.Click += RefreshButton_Click;

            Button clearLogButton = new Button
            {
                Text = "Clear Log",
                Location = new Point(235, 225),
                Size = new Size(135, 40)
            };
            clearLogButton.Click += ClearLogButton_Click;

            selectedAccountLabel = new Label
            {
                Location = new Point(25, 292),
                Size = new Size(390, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95)
            };

            transactionGroup.Controls.Add(CreateFieldLabel("Amount:", 25, 45));
            transactionGroup.Controls.Add(amountBox);
            transactionGroup.Controls.Add(CreateFieldLabel("Transfer Target:", 25, 85));
            transactionGroup.Controls.Add(transferTargetBox);
            transactionGroup.Controls.Add(depositButton);
            transactionGroup.Controls.Add(withdrawButton);
            transactionGroup.Controls.Add(transferButton);
            transactionGroup.Controls.Add(refreshButton);
            transactionGroup.Controls.Add(clearLogButton);
            transactionGroup.Controls.Add(selectedAccountLabel);
        }

        private void BuildStatusPanel()
        {
            GroupBox statusGroup = CreateGroupBox("Shared Backend Status", 30, 455, 455, 220);

            balanceLabel = CreateStatusLabel(25, 35);
            savingsLabel = CreateStatusLabel(25, 70);
            transferLabel = CreateStatusLabel(25, 105);
            atmCashLabel = CreateStatusLabel(25, 140);
            maxCashLabel = CreateStatusLabel(25, 175);

            statusGroup.Controls.Add(balanceLabel);
            statusGroup.Controls.Add(savingsLabel);
            statusGroup.Controls.Add(transferLabel);
            statusGroup.Controls.Add(atmCashLabel);
            statusGroup.Controls.Add(maxCashLabel);
        }

        private void BuildLogPanel()
        {
            GroupBox logGroup = CreateGroupBox("User Transaction Log", 525, 455, 455, 220);

            cardStatusLabel = new Label
            {
                Location = new Point(20, 30),
                Size = new Size(410, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95)
            };

            logBox = new ListBox
            {
                Location = new Point(20, 65),
                Size = new Size(410, 135)
            };

            logGroup.Controls.Add(cardStatusLabel);
            logGroup.Controls.Add(logBox);
        }

        private GroupBox CreateGroupBox(string text, int x, int y, int width, int height)
        {
            GroupBox groupBox = new GroupBox
            {
                Text = text,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(x, y),
                Size = new Size(width, height)
            };

            Controls.Add(groupBox);
            return groupBox;
        }

        private Label CreateFieldLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(130, 25)
            };
        }

        private Label CreateStatusLabel(int x, int y)
        {
            return new Label
            {
                Location = new Point(x, y),
                Size = new Size(395, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
        }

        private Label CreateSmallPanelLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(120, 25)
            };
        }

        private TextBox CreateSmallPanelTextBox(int x, int y, string text)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(220, 25),
                Text = text
            };
        }

        private Button CreateColoredButton(string text, int x, int y, int width, int height, Color backColor)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }

        private void CardTypeChanged(object sender, EventArgs e)
        {
            UpdateCardView();
        }

        private void UpdateCardView()
        {
            if (physicalRadio.Checked)
            {
                cardModeLabel.Text = "Physical mode: only the PIN is entered. Card data is read from the chip.";
                physicalCardPanel.Visible = true;
                virtualCardPanel.Visible = false;
            }
            else
            {
                cardModeLabel.Text = "Virtual mode: PIN, card number, CV2, and expiration are entered manually.";
                physicalCardPanel.Visible = false;
                virtualCardPanel.Visible = true;
            }
        }

        private void SourceAccountChanged(object sender, EventArgs e)
        {
            if (activeCard != null)
            {
                activeCard.SetAccount(GetSourceAccount());
                AddLog($"Active card source changed to {GetSourceAccount().GetAccountId()}.");
            }

            RefreshDisplay();
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

                context.SetCardNumber("1111-2222-3333-4444");
                context.SetCv2("111");
                context.SetExpirationDate("12/30");
            }
            else
            {
                factory.SetCard("Virtual");
                cardProgram = new VirtualCardProgram();

                context.SetCardNumber(cardNumberBox.Text.Trim());
                context.SetCv2(cv2Box.Text.Trim());
                context.SetExpirationDate(expirationBox.Text.Trim());
            }

            activeCard = factory.GetCard();
            activeCard.SetAccount(GetSourceAccount());

            cardProgram.SetCard(activeCard);
            cardProgram.SetContext(context);
            cardProgram.Perform();

            activeCard.SetValid();

            if (activeCard.GetValid())
            {
                AddLog($"Created a valid {activeCard.GetType().Name} card.");
                AddLog($"Card connected to {activeCard.GetAccount().GetAccountId()}.");
            }
            else
            {
                AddLog("Card validation failed.");
                MessageBox.Show("Card validation failed.");
            }

            RefreshDisplay();
        }

        private void DepositButton_Click(object sender, EventArgs e)
        {
            if (!PrepareTransaction(out Transaction transaction))
                return;

            if (!RunDefaultFilter(transaction))
            {
                AddLog("Deposit blocked by DefaultFilter.");
                return;
            }

            transaction.Deposit();
            FinishTransaction(transaction, "Deposit");
        }

        private void WithdrawButton_Click(object sender, EventArgs e)
        {
            if (!PrepareTransaction(out Transaction transaction))
                return;

            if (!RunWithdrawalFilterChain(transaction))
            {
                AddLog("Withdrawal blocked by one of the filters.");
                RefreshDisplay();
                return;
            }

            transaction.Withdrawal();
            FinishTransaction(transaction, "Withdrawal");
        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            if (!PrepareTransaction(out Transaction transaction))
                return;

            Account targetAccount = GetTransferTargetAccount();

            if (targetAccount == GetSourceAccount())
            {
                MessageBox.Show("Transfer target must be different from the source account.");
                AddLog("Transfer failed because source and target were the same.");
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
            FinishTransaction(transaction, "Transfer");
        }

        private bool PrepareTransaction(out Transaction transaction)
        {
            transaction = null;

            if (!CardReady())
                return false;

            if (!TryGetAmount(out decimal amount))
                return false;

            activeCard.SetAccount(GetSourceAccount());

            transaction = new Transaction(activeCard);
            transaction.SetAmount(amount);
            transaction.SetData(atmData);

            return true;
        }

        private void FinishTransaction(Transaction transaction, string transactionType)
        {
            if (transaction.GetStatus())
            {
                AddLog($"{transactionType} successful: {transaction.GetAmount():C}");
            }
            else
            {
                AddLog($"{transactionType} failed.");
            }

            RefreshDisplay();
        }

        private bool RunDefaultFilter(Transaction transaction)
        {
            FilterIF defaultFilter = new DefaultFilter();
            return defaultFilter.Check(transaction);
        }

        private bool RunWithdrawalFilterChain(Transaction transaction)
        {
            FilterIF defaultFilter = new DefaultFilter();

            MaximumDailyCashout maximumCashoutFilter = new MaximumDailyCashout(defaultFilter);
            CheckAvailableCash availableCashFilter = new CheckAvailableCash(maximumCashoutFilter);

            maximumCashoutFilter.SetData(atmData);
            availableCashFilter.SetData(atmData);

            if (!defaultFilter.Check(transaction))
                return false;

            if (!maximumCashoutFilter.Check(transaction))
                return false;

            if (!availableCashFilter.Check(transaction))
                return false;

            return true;
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
            if (sourceAccountBox.SelectedItem != null &&
                sourceAccountBox.SelectedItem.ToString() == "Savings")
            {
                return savingsAccount;
            }

            return checkingAccount;
        }

        private Account GetTransferTargetAccount()
        {
            string selectedTarget = transferTargetBox.SelectedItem == null
                ? "Savings"
                : transferTargetBox.SelectedItem.ToString();

            if (selectedTarget == "Checking")
                return checkingAccount;

            if (selectedTarget == "External Transfer Account")
                return externalTransferAccount;

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
            balanceLabel.Text = $"Checking Balance: {checkingAccount.GetBalance():C}";
            savingsLabel.Text = $"Savings Balance: {savingsAccount.GetBalance():C}";
            transferLabel.Text = $"External Transfer Balance: {externalTransferAccount.GetBalance():C}";
            atmCashLabel.Text = $"ATM Available Cash: {atmData.GetCash():C}";
            maxCashLabel.Text = $"ATM Max Cashout: {atmData.GetMaximumCashOut():C}";

            selectedAccountLabel.Text = $"Selected Source: {GetSourceAccount().GetAccountId()}";

            if (activeCard == null)
            {
                cardStatusLabel.Text = "Card Status: No card created";
            }
            else if (activeCard.GetValid())
            {
                cardStatusLabel.Text =
                    $"Card Status: Valid {activeCard.GetType().Name} Card | {activeCard.GetAccount().GetAccountId()}";
            }
            else
            {
                cardStatusLabel.Text = "Card Status: Invalid";
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshDisplay();
            AddLog($"Status refreshed. Company observes {company.GetATMs().Count} observable object(s).");
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            logBox.Items.Clear();
        }

        private void AddLog(string message)
        {
            logBox?.Items.Insert(0, $"{DateTime.Now:hh:mm:ss tt} - {message}");
        }
    }
}