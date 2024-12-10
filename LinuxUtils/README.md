# Linux Utilities

A collection of utilities for running Cove on Linux.

PLEASE REVIEW ALL SCRIPTS!! NEVER BLINDLY RUN SCRIPTS ON YOUR MACHINE!! PATHS WILL DEPEND ON WHERE YOUR FILES ARE!!

All scripts assume your server is located at `/home/$USER/Servers/WebFishing/publish` and that your user is not root and has sudo access.

## SystemD User Service

If you have steam autostart this service will autostart the Cove server in a tmux session on a 60 second delay.

It will also gracefully shutdown the server via systemd.

To use, just copy the `cove.service` file to `~/.config/systemd/user/default.target.wants/cove.service`. Run the following commands to correct the file permissions, let systemd know about the changes, and enable/start the service.

```bash
mkdir ~/.config/systemd/user/default.target.wants/

cp cove.service ~/.config/systemd/user/default.target.wants/

chown $USER:$USER ~/.config/systemd/user/default.target.wants/cove.service

systemctl --user daemon-reload

systemctl --user --now enable cove
```

## IP Blocklist

A set of scripts to block a set list of known VPN and Tor servers using IP tables. 

Requires both `iptables` and `iptables-persistent` on Debian 12.

You should look at whitelisting specific VPN IPs for any users that use them. (Have them stick to one VPN server location for the least amount of IP whitelisting)