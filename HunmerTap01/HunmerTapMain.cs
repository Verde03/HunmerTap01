using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace HunmerTap01
{
    public partial class HunmerTapMain : Form
    {
        public HunmerTapMain()
        {
            InitializeComponent();
        }

        // 選択肢の初期化
        string selectedWeapon = "";   // 武器
        string selectedArmor = "";    // 防具

        string selectedMines = "";    // 鉱石
        string selectedWoods = "";    // 木材
        string selectedAcsesary = ""; // 装飾石
        string selectedHunmer = "";   // ハンマー
        string selectedGruinder = ""; // 砥石
        string selectedLiquid = "";    // 焼き入れ液

        // 製品の品質と能力の限界値の初期化
        int attackPowerLimit = 0;
        int qualityLimit = 0;

        // 品質・能力の上昇値の初期値
        int qualityUpValue = 10;
        int attackPowerUpValue = 10;

        // 追加能力テキストと値の初期化
        string statusPowertext = "";
        int statusUpValue = 0;

        // 製品補正値の初期化
        int qualityCorrection = 0;
        int attackPowerCorrection = 0;

        // コマンド選択回数制限
        int countLimit = 0;

        // リストボックスにログを追加するメソッド
        private void AddLog(string message)
        {
            listBox1.Items.Add(message);
        }

        // 作る武器の選択
        private void SelectedWeapon(object sender, EventArgs e)
        {
            if (comboBox9.SelectedIndex == 0)
            {
                selectedWeapon = "ナイフ";
                attackPowerLimit = 50; // 攻撃力の上昇上限値
                qualityLimit = 50; // 品質の上限値（100％時）
            }
            if (comboBox9.SelectedIndex == 1)
            {
                selectedWeapon = "ソード";
                attackPowerLimit = 100;
                qualityLimit = 200;
            }
            if (comboBox9.SelectedIndex == 2)
            {
                selectedWeapon = "ブレイド";
                attackPowerLimit = 250;
                qualityLimit = 450;
            }
        }

        // 作る防具の選択
        private void SelectedArmor(object sender, EventArgs e)
        {
            // TODO 防具も作れるようにする
        }

        // ハンマーの選択
        private void SelectedHunmer(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                qualityCorrection = 1; // 品質の上昇補正値
                attackPowerCorrection = 0; // 能力値（攻撃力）の上昇補正値
            }
            if (comboBox1.SelectedIndex == 1)
            {
                qualityCorrection = 5;
                attackPowerCorrection = 0;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                qualityCorrection = 10;
                attackPowerCorrection = 0;
            }

            // TODO 道具の追加
        }

        // 砥石の選択
        private void SelectedGruinder(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                qualityCorrection = 0;
                attackPowerCorrection = 1;
            }
            if (comboBox3.SelectedIndex == 1)
            {
                qualityCorrection = 0;
                attackPowerCorrection = 5;
            }
            if (comboBox3.SelectedIndex == 2)
            {
                qualityCorrection = 0;
                attackPowerCorrection = 10;
            }
        }

        // 焼き入れ液の選択
        private void SelectedLiquid(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                qualityCorrection = 1;
                attackPowerCorrection = 1;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                qualityCorrection = 2;
                attackPowerCorrection = 2;
            }
            if (comboBox2.SelectedIndex == 2)
            {
                qualityCorrection = 4;
                attackPowerCorrection = 4;
            }
        }

        // 鉱石の選択
        private void SlectedMines(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == 0)
            {
                selectedMines = "ブロンズ";
                countLimit = 5; // コマンドの選択回数
            }
            if (comboBox4.SelectedIndex == 1)
            {
                selectedMines = "アイアン";
                countLimit = 10;
            }
            if (comboBox4.SelectedIndex == 2)
            {
                selectedMines = "ミスリル";
                countLimit = 20;
            }

            // コマンドボタンを有効化
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button1.Enabled = true;

            // 回数制限ラベルを更新
            UpdateCountLimitLabel();
        }

        // 木材の選択
        private void SelectedWood(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == 0)
            {
                attackPowerCorrection = 5; // 攻撃力の上昇補正値
            }
            if (comboBox5.SelectedIndex == 1)
            {
                attackPowerCorrection = 20;
            }
            if (comboBox5.SelectedIndex == 2)
            {
                attackPowerCorrection = 50;
            }
        }

        // 装飾石の選択
        private void SelecedAcsesary(object sender, EventArgs e)
        {
            // 製品に能力値付与
            if (comboBox6.SelectedIndex == 0)
            {
                selectedAcsesary = "無属の";
                statusPowertext = "(+1)";
                statusUpValue = 1;
            }
            if (comboBox6.SelectedIndex == 1)
            {
                selectedAcsesary = "赤熱の";
                statusPowertext = "\n追加ダメージ：火(5) 攻撃+1%";
                statusUpValue = (int)(attackPowerUpValue * 0.01) + 5;
            }
            if (comboBox6.SelectedIndex == 2)
            {
                selectedAcsesary = "流水の";
                statusPowertext = "\n追加ダメージ：水(5) 命中+2";
                statusUpValue = 5;
            }
            if (comboBox6.SelectedIndex == 3)
            {
                selectedAcsesary = "疾風の";
                statusPowertext = "\n追加ダメージ：風(5) 攻撃速度+1%";
                statusUpValue = 5;
            }
            if (comboBox6.SelectedIndex == 4)
            {
                selectedAcsesary = "岩硬の";
                statusPowertext = "\n追加ダメージ：土(5) 防御力+1%";
                statusUpValue = 5;
            }
            if (comboBox6.SelectedIndex == 5)
            {
                selectedAcsesary = "紫電の";
                statusPowertext = "\n追加ダメージ：雷(5) クリティカル率+1%";
                statusUpValue = 5;
            }
            if (comboBox6.SelectedIndex == 6)
            {
                selectedAcsesary = "氷雪の";
                statusPowertext = "\n追加ダメージ：氷(5) 敵攻撃速度DOWN";
                statusUpValue = 5;
            }
            if (comboBox6.SelectedIndex == 7)
            {
                selectedAcsesary = "閃光の";
                statusPowertext = "\n追加ダメージ：光(5) 自動回復+1";
                statusUpValue = 5;
            }
            if (comboBox6.SelectedIndex == 8)
            {
                selectedAcsesary = "暗光の";
                statusPowertext = "\n追加ダメージ：闇(5) 敵命中-2";
                statusUpValue = 5;
            }
        }

        // プログレスバーの初期設定
        private void InitializeProgressBar()
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 50 + qualityLimit; // この値は品質の合計の最大値に基づいて調整する
            progressBar1.Value = 0;

            progressBar2.Minimum = 0;
            progressBar2.Maximum = 0 + attackPowerLimit;
            progressBar2.Value = 0;
        }

        // 各選択肢に応じてプログレスバーを更新
        private void UpdateQuarityProgressBar1()
        {
            int previousValue = progressBar1.Value;
            progressBar1.Value = Math.Min(qualityUpValue, progressBar1.Maximum);

            // ログの追加
            AddLog($"品質が更新されました: 現在の値 {progressBar1.Value} (増加数 {progressBar1.Value - previousValue})");
        }

        private void UpdateQuarityProgressBar2()
        {
            int previousValue = progressBar2.Value;
            progressBar2.Value = Math.Min(attackPowerUpValue, progressBar2.Maximum);

            // ログの追加
            AddLog($"攻撃力が更新されました: 現在の値 {progressBar2.Value} (増加数 {progressBar2.Value - previousValue})");
        }

        // ハンマーで作業したとき
        private void HunmerClickd(object sender, EventArgs e)
        {
            if (countLimit > 0)
            {
                countLimit--;
                // ハンマーの処理
                if (qualityUpValue + qualityCorrection <= qualityLimit)
                {
                    qualityUpValue += (qualityUpValue + qualityCorrection) / 2;
                }
                UpdateQuarityProgressBar1();
                // 回数制限ラベルを更新
                UpdateCountLimitLabel();
            }
            // ボタンの有効/無効状態を更新
            UpdateButtonState();
        }

        // 砥石で研磨したとき
        private void GruinderClickd(object sender, EventArgs e)
        {
            if (countLimit > 0)
            {
                countLimit--;
                // 砥石の処理
                if (attackPowerUpValue + attackPowerCorrection <= attackPowerLimit)
                {
                    attackPowerUpValue += (attackPowerUpValue + attackPowerCorrection) / 2;
                }
                UpdateQuarityProgressBar2();
                // 回数制限ラベルを更新
                UpdateCountLimitLabel();
            }
            // ボタンの有効/無効状態を更新
            UpdateButtonState();
        }

        // 焼き入れしたとき
        private void QuenchingClickd(object sender, EventArgs e)
        {
            if (countLimit > 0)
            {
                countLimit--;
                // 焼き入れの処理
                // 焼き入れをすると品質と能力値の両方が上がる
                if (qualityUpValue + qualityCorrection <= qualityLimit)
                {
                    qualityUpValue += (qualityUpValue + qualityCorrection) / 2;
                }
                if (attackPowerUpValue + attackPowerCorrection <= attackPowerLimit)
                {
                    attackPowerUpValue += (attackPowerUpValue + attackPowerCorrection) / 2;
                }
                UpdateQuarityProgressBar2();
                UpdateQuarityProgressBar1();
                // 回数制限ラベルを更新
                UpdateCountLimitLabel();
            }
            // ボタンの有効/無効状態を更新
            UpdateButtonState();
        }

        // 値段を決定
        private int CalculatePrice(int quality, int attackPower)
        {
            int basePrice = 0;
            int basePrice2 = 0;
            if (comboBox9.SelectedIndex == 0)
            {
                basePrice = 10; // 短剣の基本価格
            }
            if (comboBox9.SelectedIndex == 1)
            {
                basePrice = 50; // 長剣の基本価格
            }
            if (comboBox9.SelectedIndex == 2)
            {
                basePrice = 100; // 両手剣の基本価格
            }
            if (comboBox4.SelectedIndex == 0)
            {
                basePrice2 = 50; // 銅鉱石の基本価格
            }
            if (comboBox4.SelectedIndex == 1)
            {
                basePrice2 = 100; // 鉄鉱石の基本価格
            }
            if (comboBox4.SelectedIndex == 2)
            {
                basePrice2 = 200; // ミスリル鉱石の基本価格
            }

            int qualityMultiplier = 2; // 品質の係数
            int attackPowerMultiplier = 3; // 攻撃力の係数
            int acsesaryMultiplier = 200; // 装飾品の有無の価値

            if (comboBox6.SelectedIndex >= 0)
            {
                int price = basePrice + basePrice2 + (quality * qualityMultiplier) + (attackPower * attackPowerMultiplier) + acsesaryMultiplier;
                return price;
            }
            else
            {
                int price = basePrice + basePrice2 + (quality * qualityMultiplier) + (attackPower * attackPowerMultiplier);
                return price;
            }
        }

        // 製品を完成させたとき
        private void CompleteProductClickd(object sender, EventArgs e)
        {
            if (progressBar1.Value <= 30)
            {
                // 失敗メッセージの生成
                string completionMessage1 = "製作に失敗しました";
                // メッセージボックスで表示
                MessageBox.Show(completionMessage1, "製造失敗");

                // ログの追加
                AddLog(completionMessage1);
            }
            else
            {
                // 製品の価格を計算
                int finalPrice = CalculatePrice(qualityUpValue, attackPowerUpValue);

                // 完成メッセージの生成
                string completionMessage = $"{selectedAcsesary}{selectedMines}{selectedWeapon}が完成しました：攻撃力＋{(int)(attackPowerUpValue) + statusUpValue}{statusPowertext}。\n価格: {finalPrice}G";

                // メッセージボックスで表示
                MessageBox.Show(completionMessage, "製品完成");

                // ログの追加
                AddLog(completionMessage);
            }

        }

        // ボタンの有効/無効状態を更新するメソッド
        private void UpdateButtonState()
        {
            button3.Enabled = countLimit > 0;
            button4.Enabled = countLimit > 0;
            button5.Enabled = countLimit > 0;

            // 工数がリミットに達した場合
            if (button3.Enabled == false && button4.Enabled == false && button5.Enabled == false)
            {
                // 工数リミットメッセージの生成
                string completionMessage2 = "実行可能な工数がなくなりました";

                // メッセージボックスで表示
                MessageBox.Show(completionMessage2, "工数リミット");

                // ログの追加
                AddLog(completionMessage2);
            }
        }

        private void CounterLimitChanged(object sender, EventArgs e)
        {
            // ラベルの値を整数に変換し、countLimitに設定
            if (int.TryParse(label13.Text, out int newLimit))
            {
                countLimit = newLimit;
                // 必要な処理をここに追加
            }
        }
        // 回数制限ラベルを更新するメソッド
        private void UpdateCountLimitLabel()
        {
            label13.Text = countLimit.ToString();
        }
    }
}