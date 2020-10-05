import React, { useState } from "react";
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Collapse from '@material-ui/core/Collapse';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    root: {
        width: '100%',
        maxWidth: 360,
        backgroundColor: theme.palette.background.paper,
    },
    nested: {
        paddingLeft: theme.spacing(4),
    },
}));

const Foundation = ({ foundation }) => {
    const [open, setOpen] = useState(false);
    const programs = foundation.Programs || [];
    const classes = useStyles();

    const getStatus = status => {
        return status == 0 ? "Open" : "Closed";
    }
    return (
        <ListItem className='foundation-item'>
            <h4>{foundation.FoundationName}</h4>
            <ListItem button onClick={() => setOpen(!open)}>
                {open ? <a>{'^'}</a> : <a>{'V'}</a>}
            </ListItem>
            <Collapse in={open} timeout="auto" unmountOnExit>
                <List component="div" disablePadding>
                    {programs.map(program =>
                        <ListItem className={classes.nested}>
                            <ListItemText primary={program.AssistanceProgramName} />
                            <ListItemText primary={getStatus(program.ProgramStatus)} />
                            <ListItemText primary={program.GrantAmount} />
                        </ListItem >)
                    }
                </List>
            </Collapse>
        </ListItem >
    );
};
export default Foundation;