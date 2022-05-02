set -x
set -v
grep -i -r  . --exclude-dir "mnt" -e $1
