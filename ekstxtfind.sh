set -x
set -v
grep -i -n -r  . --exclude-dir "mnt" -e $1
