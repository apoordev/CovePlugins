#!/bin/bash
echo "Unblocking VPNs"
sleep 2
while read line; do sudo iptables -D INPUT -s $line -j DROP; done < /home/$USER/Servers/blockedips/vpnipv4.txt
echo "Unblocking Tor"
sleep 2
while read line; do sudo iptables -D INPUT -s $line -j DROP; done < /home/$USER/Servers/blockedips/toripv4.txt
