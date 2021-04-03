# XingKongScreenSaver

## 一、简介
在采用 `树莓派4B + 微雪 4inch DPI LCD` 的组合来实现项目展示的时候，发现 Raspbian 系统的屏幕保护功能无法正常工作，若用户在一段时间之内没有操作，系统无法自动关闭屏幕

在苦苦搜索无果后，决定自己写一个程序，实现空闲时自动息屏，用户操作时自动亮屏功能

推测可能与 DPI 屏幕有关，HDMI 屏幕应该无此问题

## 二、安装
`XingKongScreenSaver` 需要依赖 `xprintidle` 才能正常运行
```Shell
sudo apt-get install xprintidle
```

编译或下载 `xkscreensaver` 可执行文件，拷贝到 `/home/pi/` 目录下
>注意：<br>
直接编译完成后的文件名为 `XKScreenSaver`，方便起见，建议重命名为小写 `xkscreensaver`

然后运行

```Shell
sudo nano /usr/lib/systemd/system/xkscreensaver.service
```

在文本编辑器中输入下面的内容，保存后退出

```Shell
[Unit]
Description=xkscreensaver

[Service]
Type=simple
User=pi
Group=pi
LimitNOFILE=100000
LimitNPROC=100000
ExecStart=/home/pi/xkscreensaver
StartLimitIntervalSec=0
Restart=always
RestartSec=1
StandardOutput=null

[Install]
WantedBy=multi-user.target

```

通知系统重新加载服务
```Shell
sudo systemctl daemon-reload
```

设置服务为开机自启
```Shell
sudo systemctl enable xkscreensaver
```

重新启动树莓派
```Shell
sudo reboot
```