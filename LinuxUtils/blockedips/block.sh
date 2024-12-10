#!/bin/bash
echo "Blocking VPNs"
sleep 2
while read line; do sudo iptables -A INPUT -s $line -j DROP; done < /home/$USER/Servers/blockedips/vpnipv4.txt
echo "Blocking Tor"
sleep 2
while read line; do sudo iptables -A INPUT -s $line -j DROP; done < /home/$USER/Servers/blockedips/toripv4.txt
