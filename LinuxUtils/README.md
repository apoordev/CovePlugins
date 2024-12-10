# Linux Utilities

A collection of utilities for running Cove on Linux.

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
