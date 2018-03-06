SRC_DIR := src/cli
DEST_DIR := $(SRC_DIR)/build
ARTIFACT_NAME := cli
REQUIREMENTS_FILE := $(SRC_DIR)/requirements.txt

cli: clean build install_requirements copy

build:
	mkdir -p $(DEST_DIR)
	zip -rj $(DEST_DIR)/$(ARTIFACT_NAME).tmp $(SRC_DIR)/*
	echo "#!/usr/bin/env python" | cat - $(DEST_DIR)/$(ARTIFACT_NAME).tmp > $(DEST_DIR)/$(ARTIFACT_NAME)
	rm $(DEST_DIR)/$(ARTIFACT_NAME).tmp
	chmod +x $(DEST_DIR)/$(ARTIFACT_NAME)

install_requirements:
	pip install --user -r $(REQUIREMENTS_FILE)

copy:
	cp $(DEST_DIR)/$(ARTIFACT_NAME) $(PWD)/$(ARTIFACT_NAME)

clean:
	rm -rf $(DEST_DIR)