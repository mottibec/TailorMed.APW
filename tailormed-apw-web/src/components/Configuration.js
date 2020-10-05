import React, { useEffect, useState } from "react";
import { SetFoundationScraperRunStatus } from "../api/api";

const Configuration = ({ config }) => {
    const updateFoundationStatus = (e) => {
        SetFoundationScraperRunStatus(config.id, e.checked);
    }
    return (
        <form>
            <input type="text" value={config.FoundationName} />
            <input type="text" value={config.Url} />
            <input type="text" value={config.ProgramListExtractor} />
            <input type="text" value={config.ProgramDetailsLinkExtractor} />
            <input type="text" value={config.NameExtractor} />
            <input type="text" value={config.AmountExtractor} />
            <input type="text" value={config.StatusExtractor} />
            <input type="checkbox" checked={config.IsActive} onChange={updateFoundationStatus} />
        </form>
    );
}

export default Configuration;