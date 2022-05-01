echo "arguments: mono version (e.g. runtime, devel, complete, dbg)"
echo $1
sudo apt remove -y *mono*
sudo rm -f /etc/apt/sources.list.d/mono-official-stable.list
sudo apt --fix-broken install -y
sudo apt-get autoremove -y
sudo apt install -y gnupg ca-certificates
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb https://download.mono-project.com/repo/ubuntu stable-focal main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list
sudo apt update -y
sudo apt install -y mono-$1
sudo apt install -y gtk-sharp2
