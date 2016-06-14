using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Calculator_function : MonoBehaviour {

    public bool no_alterable = false;//0 - num_1; 2 - dec_1; 2 - num_2; 3 - dec_2
    public int spaces = 0;
    public bool w2 = false;
    public double num_1 = 0;
    public bool dp_1 = false;
    public int dn_1 = 0;
    public double num_2 = 0;
    public bool dp_2 = false;
    public int dn_2 = 0;
    public int op_tp = -1;
    public double result = 0;
    private string num1 = "";
    private string num2 = "";


    private double[,] state_num;//0 - num_1; 1 - num_2
    private bool[,] state_bool;//0 - w2; 1 - dp_1; 2 - dp_2
    private int[,] state_int;//0 - dn_1; 1 - dn_2; 2 - op_tp
    private string[,] state_string;//0 - num1; 1 - num2
    // Use this for initialization

    public string[,] hist = new string[10,2];//0 - operation; 1 - Result
    private bool browsing_history = false;
    public int position_in_history = -1;
    public int operations_saved = 0;

    private TextMesh o_writer;
    private TextMesh r_writer;

    public GameObject log;
//    private Log_writer lg;

    void Start() {
        //lg = log.GetComponent<Log_writer>();

        o_writer = transform.GetChild(20).GetChild(0).GetComponent<TextMesh>(); 
        r_writer = transform.GetChild(20).GetChild(1).GetComponent<TextMesh>();

        o_writer.text = "";
        r_writer.text = "";
                            
        hist = new string[10, 2];
                
        state_num = new double[20, 2];
        state_bool = new bool[20, 3];
        state_int = new int[20, 3];
        state_string = new string[20, 2];

        state_num[0, 0] = num_1;
        state_num[0, 1] = num_2;

        state_bool[0, 0] = w2;
        state_bool[0, 1] = dp_1;
        state_bool[0, 2] = dp_2;

        state_int[0, 0] = dn_1;
        state_int[0, 1] = dn_2;
        state_int[0, 2] = op_tp;

        state_string[0, 0] = "";
        state_string[0, 1] = "";
    }

    // Update is called once per frame
    void Update() {

    }

    public void write_number(int num) {
        if (browsing_history) {
            r_writer.text = "";
            browsing_history = false;
            position_in_history = -1;
        }
        if (no_alterable){ clear_operation(); r_writer.text = ""; }
        if (spaces <= 18) {
            if (!w2) {
                if (!dp_1) {
                    num_1 = num_1 * 10 + num;
                    num1 = num1 + num.ToString();
                    spaces++;
                    state_num[spaces, 0] = num_1;
                    state_num[spaces, 1] = num_2;

                    state_bool[spaces, 0] = w2;
                    state_bool[spaces, 1] = dp_1;
                    state_bool[spaces, 2] = dp_2;

                    state_int[spaces, 0] = dn_1;
                    state_int[spaces, 1] = dn_2;
                    state_int[spaces, 2] = op_tp;

                    state_string[spaces, 0] = num1;
                    state_string[spaces, 1] = num2;
                }
                else {
                    dn_1++;
                    num_1 = num_1 + num / Mathf.Pow(10, dn_1);
                    num1 = num1 + num.ToString();
                    spaces++;
                    state_num[spaces, 0] = num_1;
                    state_num[spaces, 1] = num_2;

                    state_bool[spaces, 0] = w2;
                    state_bool[spaces, 1] = dp_1;
                    state_bool[spaces, 2] = dp_2;

                    state_int[spaces, 0] = dn_1;
                    state_int[spaces, 1] = dn_2;
                    state_int[spaces, 2] = op_tp;

                    state_string[spaces, 0] = num1;
                    state_string[spaces, 1] = num2;
                }
            }
            else {
                if (!dp_2) {
                    num_2 = num_2 * 10 + num;
                    num2 = num2 + num.ToString();
                    spaces++;
                    state_num[spaces, 0] = num_1;
                    state_num[spaces, 1] = num_2;

                    state_bool[spaces, 0] = w2;
                    state_bool[spaces, 1] = dp_1;
                    state_bool[spaces, 2] = dp_2;

                    state_int[spaces, 0] = dn_1;
                    state_int[spaces, 1] = dn_2;
                    state_int[spaces, 2] = op_tp;

                    state_string[spaces, 0] = num1;
                    state_string[spaces, 1] = num2;
                }
                else {
                    dn_2++;
                    num2 = num2 + num.ToString();
                    num_2 = num_2 + num / Mathf.Pow(10, dn_2);
                    spaces++;
                    state_num[spaces, 0] = num_1;
                    state_num[spaces, 1] = num_2;

                    state_bool[spaces, 0] = w2;
                    state_bool[spaces, 1] = dp_1;
                    state_bool[spaces, 2] = dp_2;

                    state_int[spaces, 0] = dn_1;
                    state_int[spaces, 1] = dn_2;
                    state_int[spaces, 2] = op_tp;

                    state_string[spaces, 0] = num1;
                    state_string[spaces, 1] = num2;
                }
            }
            this.write_Operation();
        }
    }

    public void write_decimal_point() {
        if (browsing_history)
        {
            r_writer.text = "";
            browsing_history = false;
            position_in_history = -1;
        }
        if (spaces <= 19) {
            if (!w2) {
                if (!dp_1) {
                    spaces++;
                    num1 = num1 + ".";
                }
                dp_1 = true;
                state_num[spaces, 0] = num_1;
                state_num[spaces, 1] = num_2;

                state_bool[spaces, 0] = w2;
                state_bool[spaces, 1] = dp_1;
                state_bool[spaces, 2] = dp_2;

                state_int[spaces, 0] = dn_1;
                state_int[spaces, 1] = dn_2;
                state_int[spaces, 2] = op_tp;

                state_string[spaces, 0] = num1;
                state_string[spaces, 1] = num2;
            }
            else {
                if (!dp_2) {
                    spaces++;
                    num2 = num2 + ".";
                }
                dp_2 = true;
                state_num[spaces, 0] = num_1;
                state_num[spaces, 1] = num_2;

                state_bool[spaces, 0] = w2;
                state_bool[spaces, 1] = dp_1;
                state_bool[spaces, 2] = dp_2;

                state_int[spaces, 0] = dn_1;
                state_int[spaces, 1] = dn_2;
                state_int[spaces, 2] = op_tp;

                state_string[spaces, 0] = num1;
                state_string[spaces, 1] = num2;
            }
            this.write_Operation();
        }
        if (no_alterable) { this.clear_operation(); this.write_Operation(); r_writer.text = ""; }

    }

    public void write_operation(int op_t){
        if (browsing_history)
        {
            r_writer.text = "";
            browsing_history = false;
            position_in_history = -1;
        }
        if (spaces <= 19) {
            if (num_2 == 0) {
                if (!w2) { spaces++; }
                op_tp = op_t;
                w2 = true;

                this.write_Operation();

                state_num[spaces, 0] = num_1;
                state_num[spaces, 1] = num_2;

                state_bool[spaces, 0] = w2;
                state_bool[spaces, 1] = dp_1;
                state_bool[spaces, 2] = dp_2;

                state_int[spaces, 0] = dn_1;
                state_int[spaces, 1] = dn_2;
                state_int[spaces, 2] = op_tp;

                state_string[spaces, 0] = num1;
                state_string[spaces, 1] = num2;

                
            }
        }
        if (no_alterable) { this.clear_operation(); r_writer.text = ""; }
    }

    public void clear_operation() {
        if (browsing_history)
        {
            r_writer.text = "";
            browsing_history = false;
            position_in_history = -1;
        }
        no_alterable = false;//0 - num_1; 2 - dec_1; 2 - num_2; 3 - dec_2
        spaces = 0;
        w2 = false;
        num_1 = 0;
        dp_1 = false;
        dn_1 = 0;
        num_2 = 0;
        dp_2 = false;
        dn_2 = 0;
        op_tp = 0;
        result = 0;
        num1 = "";
        num2 = "";

        state_num = new double[20, 2];
        state_bool = new bool[20, 3];
        state_int = new int[20, 3];

        state_num[0, 0] = num_1;
        state_num[0, 1] = num_2;

        state_bool[0, 0] = w2;
        state_bool[0, 1] = dp_1;
        state_bool[0, 2] = dp_2;

        state_int[0, 0] = dn_1;
        state_int[0, 1] = dn_2;
        state_int[0, 2] = op_tp;

        state_string[0, 0] = num1;
        state_string[0, 1] = num2;

        this.write_Operation();
        this.erease_Result();
    }

    public void erease() {
        if (browsing_history)
        {
            r_writer.text = "";
            browsing_history = false;
            position_in_history = -1;
        }
        if (spaces > 0)
        {
            spaces--;

            num_1 = state_num[spaces, 0];
            num_2 = state_num[spaces, 1];

            w2 = state_bool[spaces, 0];
            dp_1 = state_bool[spaces, 1];
            dp_2 = state_bool[spaces, 2];

            dn_1 = state_int[spaces, 0];
            dn_2 = state_int[spaces, 1];
            op_tp = state_int[spaces, 2];

            num1 = state_string[spaces, 0];
            num2 = state_string[spaces, 1];

            this.write_Operation();
        }
        if (no_alterable) {
            this.clear_operation();
            this.write_Operation();
            this.erease_Result();
        }
    }

    public void solve()
    {
        if (browsing_history)
        {
            r_writer.text = "";
            browsing_history = false;
            position_in_history = -1;
        }
        if (num_2 != 0 && !no_alterable)
        {
            if (op_tp - 1 == 0) {
                result = num_1 + num_2;
//				try{
//                	lg.write_event("Realizo la siguiente operacion : "+num1 + " + " + num2 + " = " + result);
//				}
//				catch(UnityException e){}
                no_alterable = true;
            }
            else if (op_tp - 1 == 1) {
                result = num_1 - num_2;
//				try{
//                	lg.write_event("Realizo la siguiente operacion : " + num1 + " - " + num2 + " = " + result);
//				}
//				catch(UnityException e){}
                no_alterable = true;
            }
            else if (op_tp - 1 == 2) {
                result = num_1 * num_2;
//				try{
//                	lg.write_event("Realizo la siguiente operacion : " + num1 + " * " + num2 + " = " + result);
//				}
//				catch(UnityException e){}
                no_alterable = true;
            }
            else if (op_tp - 1 == 3) {
                result = num_1 / num_2;
//				try{
//                	lg.write_event("Realizo la siguiente operacion : " + num1 + " / " + num2 + " = " + result);
//				}
//				catch(UnityException e){}
                no_alterable = true;
            }
            this.write_Result();
            this.save_in_history();
            //log guarda accion
            
        }
        else if (no_alterable)
        {
            this.clear_operation();
            this.write_Operation();
            this.erease_Result();
        }
    }

    public void up_history() {
        if (!browsing_history) { this.clear_operation(); }
        r_writer.text = "";
        browsing_history = true;
        position_in_history++;
        if (position_in_history > operations_saved-1) { position_in_history = operations_saved - 1; }
        o_writer.text = hist[position_in_history, 0];
        r_writer.text = hist[position_in_history, 1];
    }

    public void down_history() {
        browsing_history = true;
        position_in_history--;
        if (position_in_history < 0) { position_in_history = 0; }
        o_writer.text = hist[position_in_history, 0];
        r_writer.text = hist[position_in_history, 1];
    }

    private void save_in_history() {
        operations_saved++;
        if (operations_saved >= 10) { operations_saved = 10; }

        if (operations_saved == 2)
        {
            hist[1, 0] = hist[0, 0];
            hist[1, 1] = hist[0, 1];
        }
        if (operations_saved == 3)
        {
            hist[2, 0] = hist[1, 0];
            hist[2, 1] = hist[1, 1];
            hist[1, 0] = hist[0, 0];
            hist[1, 1] = hist[0, 1];
        }
        if (operations_saved == 4)
        {
            hist[3, 0] = hist[2, 0];
            hist[3, 1] = hist[2, 1];
            hist[2, 0] = hist[1, 0];
            hist[2, 1] = hist[1, 1];
            hist[1, 0] = hist[0, 0];
            hist[1, 1] = hist[0, 1];
        }
        if (operations_saved == 5)
        {
            hist[4, 0] = hist[3, 0];
            hist[4, 1] = hist[3, 1];
            hist[3, 0] = hist[2, 0];
            hist[3, 1] = hist[2, 1];
            hist[2, 0] = hist[1, 0];
            hist[2, 1] = hist[1, 1];
            hist[1, 0] = hist[0, 0];
            hist[1, 1] = hist[0, 1];
        }
        if (operations_saved == 6)
        {
            hist[5, 0] = hist[4, 0];
            hist[5, 1] = hist[4, 1];
            hist[4, 0] = hist[3, 0];
            hist[4, 1] = hist[3, 1];
            hist[3, 0] = hist[2, 0];
            hist[3, 1] = hist[2, 1];
            hist[2, 0] = hist[1, 0];
            hist[2, 1] = hist[1, 1];
            hist[1, 0] = hist[0, 0];
            hist[1, 1] = hist[0, 1];
        }
        if (operations_saved == 7)
        {
            hist[6, 0] = hist[5, 0];
            hist[6, 1] = hist[5, 1];
            hist[5, 0] = hist[4, 0];
            hist[5, 1] = hist[4, 1];
            hist[4, 0] = hist[3, 0];
            hist[4, 1] = hist[3, 1];
            hist[3, 0] = hist[2, 0];
            hist[3, 1] = hist[2, 1];
            hist[2, 0] = hist[1, 0];
            hist[2, 1] = hist[1, 1];
            hist[1, 0] = hist[0, 0];
            hist[1, 1] = hist[0, 1];
        }
        if (operations_saved == 8)
        {
            hist[7, 0] = hist[6, 0];
            hist[7, 1] = hist[6, 1];
            hist[6, 0] = hist[5, 0];
            hist[6, 1] = hist[5, 1];
            hist[5, 0] = hist[4, 0];
            hist[5, 1] = hist[4, 1];
            hist[4, 0] = hist[3, 0];
            hist[4, 1] = hist[3, 1];
            hist[3, 0] = hist[2, 0];
            hist[3, 1] = hist[2, 1];
            hist[2, 0] = hist[1, 0];
            hist[2, 1] = hist[1, 1];
            hist[1, 0] = hist[0, 0];
            hist[1, 1] = hist[0, 1];
        }
        if (operations_saved == 9)
        {
            hist[8, 0] = hist[7, 0];
            hist[8, 1] = hist[7, 1];
            hist[7, 0] = hist[6, 0];
            hist[7, 1] = hist[6, 1];
            hist[6, 0] = hist[5, 0];
            hist[6, 1] = hist[5, 1];
            hist[5, 0] = hist[4, 0];
            hist[5, 1] = hist[4, 1];
            hist[4, 0] = hist[3, 0];
            hist[4, 1] = hist[3, 1];
            hist[3, 0] = hist[2, 0];
            hist[3, 1] = hist[2, 1];
            hist[2, 0] = hist[1, 0];
            hist[2, 1] = hist[1, 1];
            hist[1, 0] = hist[0, 0];
            hist[1, 1] = hist[0, 1];
        }
        if (operations_saved == 10)
        {
            hist[9, 0] = hist[7, 0];
            hist[9, 1] = hist[7, 1];
            hist[8, 0] = hist[7, 0];
            hist[8, 1] = hist[7, 1];
            hist[7, 0] = hist[6, 0];
            hist[7, 1] = hist[6, 1];
            hist[6, 0] = hist[5, 0];
            hist[6, 1] = hist[5, 1];
            hist[5, 0] = hist[4, 0];
            hist[5, 1] = hist[4, 1];
            hist[4, 0] = hist[3, 0];
            hist[4, 1] = hist[3, 1];
            hist[3, 0] = hist[2, 0];
            hist[3, 1] = hist[2, 1];
            hist[2, 0] = hist[1, 0];
            hist[2, 1] = hist[1, 1];
            hist[1, 0] = hist[0, 0];
            hist[1, 1] = hist[0, 1];
        }

        hist[0, 0] = o_writer.text;
        hist[0, 1] = r_writer.text;

    }

    private void write_Operation()
    {
        string op_aux = "";
        op_aux = op_aux + num1;
        if (op_tp - 1 == 0) {
            op_aux = op_aux + "+";
        }
        else if (op_tp - 1 == 1) {
            op_aux = op_aux + "-";
        }
        else if (op_tp - 1 == 2)
            {
                op_aux = op_aux + "x";
            }
        else if (op_tp - 1 == 3)
            {
                op_aux = op_aux + "÷";
            }
        op_aux = op_aux + num2;
        o_writer.text = op_aux;
    }

    private void write_Result()
    {
        string r_aux = result.ToString();
        if ( r_aux.Length > 12)
        {
            r_aux = r_aux.Substring(0, 12);
        }
        r_writer.text = r_aux;
    }

    private void erease_Result()
    {
        r_writer.text = "";
    }

    
}
