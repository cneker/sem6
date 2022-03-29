package com.example.lab09jv;

import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ImageButton;

public class MainActivity extends Activity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        OnClickListener btnClick=new OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.i("myLogs",v.getId()+"");
                Click(v.getId());
            }
        };
        findViewById(R.id.imageButton4).setOnClickListener(btnClick);
        findViewById(R.id.imageButton2).setOnClickListener(btnClick);
        findViewById(R.id.imageButton3).setOnClickListener(btnClick);
    }
    protected void Click(int view){
        Intent intent=null;
        Log.i("myLogs",view+"");
        switch (view){
            case R.id.imageButton4: intent=new Intent(this,MediaActivity.class);   break;
            case R.id.imageButton3: intent=new Intent(this,GalleryActivity.class); break;
            case R.id.imageButton2: intent=new Intent(this,CameraActivity.class); break;
            default: break;
        }
        if(intent!=null){
            Log.i("myLogs","Интент = "+ intent);
            startActivity(intent);
        }
    }
}